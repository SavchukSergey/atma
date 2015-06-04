using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Atmega.Flasher.Hex;
using Atmega.Hex;

namespace Atmega.Flasher.Models {
    public class FlasherModel : INotifyPropertyChanged {

        private HexBoard _eepromHexBoard = new HexBoard();
        private HexBoard _flashHexBoard = new HexBoard();

        public FlasherModel() {
            _eepromHexBoard[0] = null;
            _flashHexBoard[0] = null;
        }

        public void OpenFile(string filePath) {
            var hexFile = HexFile.Load(filePath);
            FlashHexBoard = HexBoard.From(hexFile);
            //todo: split eeprom & flash
        }

        public HexBoard EepromHexBoard {
            get {
                return _eepromHexBoard;
            }
            set {
                _eepromHexBoard = value;
                OnPropertyChanged();
            }
        }

        public HexBoard FlashHexBoard {
            get {
                return _flashHexBoard;
            }
            set {
                _flashHexBoard = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null) {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool ReadDevice(DeviceOperation op, CancellationToken cancellationToken) {
            var config = FlasherConfig.Read();
            var device = config.Device;
            var flashSize = device.Flash.Size;
            var eepromSize = device.EepromSize;
            op.FlashSize += flashSize;
            op.EepromSize += eepromSize;

            var flashData = new byte[flashSize];
            var eepData = new byte[eepromSize];
            using (var programmer = CreateProgrammer(op, cancellationToken)) {
                programmer.Start();
                programmer.ReadPage(0, AvrMemoryType.Flash, flashData, 0, flashSize);
                programmer.ReadPage(0, AvrMemoryType.Eeprom, eepData, 0, eepromSize);
                programmer.Stop();
            }
            op.CurrentState = "Everything is done";

            EepromHexBoard = HexBoard.From(eepData);
            FlashHexBoard = HexBoard.From(flashData);

            return true;
        }

        public bool WriteDevice(DeviceOperation op, CancellationToken cancellationToken) {
            var config = FlasherConfig.Read();
            var device = config.Device;

            var eepromBlocks = EepromHexBoard.SplitBlocks();
            var flashBlocks = FlashHexBoard.SplitBlocks(device.Flash.PageSize);

            op.FlashSize += flashBlocks.TotalBytes;
            op.EepromSize += eepromBlocks.TotalBytes;

            using (var programmer = CreateProgrammer(op, cancellationToken)) {
                programmer.Start();

                foreach (var block in flashBlocks.Blocks) {
                    programmer.WritePage(block.Address, AvrMemoryType.Flash, block.Data, 0, block.Data.Length);
                }

                foreach (var block in eepromBlocks.Blocks) {
                    programmer.WritePage(block.Address, AvrMemoryType.Eeprom, block.Data, 0, block.Data.Length);
                }

                programmer.Stop();
            }
            op.CurrentState = "Everything is done";

            return true;
        }

        public bool VerifyDevice(DeviceOperation op, CancellationToken cancellationToken) {
            var eepromBlocks = EepromHexBoard.SplitBlocks();
            var flashBlocks = FlashHexBoard.SplitBlocks();

            op.FlashSize += flashBlocks.TotalBytes;
            op.EepromSize += eepromBlocks.TotalBytes;

            using (var programmer = CreateProgrammer(op, cancellationToken)) {
                programmer.Start();

                if (!VerifyBlocks(programmer, flashBlocks, AvrMemoryType.Flash, op)) {
                    return false;
                }

                if (!VerifyBlocks(programmer, eepromBlocks, AvrMemoryType.Eeprom, op)) {
                    return false;
                }

                programmer.Stop();
            }
            op.Complete();
            op.CurrentState = "Everything is done";

            return true;
        }

        public bool EraseDevice(DeviceOperation op, CancellationToken cancellationToken) {
            var eepromBlocks = EepromHexBoard.SplitBlocks();
            var flashBlocks = FlashHexBoard.SplitBlocks();

            op.FlashSize += flashBlocks.TotalBytes;
            op.EepromSize += eepromBlocks.TotalBytes;

            using (var programmer = CreateProgrammer(op, cancellationToken)) {
                programmer.Start();

                programmer.EraseDevice();

                programmer.Stop();
            }

            op.Complete();
            op.CurrentState = "Everything is done";

            return true;
        }

        private static bool VerifyBlocks(IProgrammer programmer, HexBlocks blocks, AvrMemoryType memType, DeviceOperation op) {
            if (blocks.Blocks.All(block => VerifyBlock(programmer, block, memType))) {
                return true;
            }
            op.Complete();
            op.CurrentState = string.Format("{0} memory verification failed", memType);
            op.Status = DeviceOperationStatus.Error;
            return false;
        }

        private static bool VerifyBlock(IProgrammer programmer, HexBlock block, AvrMemoryType memType) {
            var actual = new byte[block.Data.Length];
            programmer.ReadPage(block.Address, memType, actual, 0, actual.Length);
            return !block.Data.Where((t, i) => t != actual[i]).Any();
        }

        public async Task<bool> ReadDeviceAsync(DeviceOperation op, CancellationToken cancellationToken) {
            return await Task.Run(() => ReadDevice(op, cancellationToken), cancellationToken);
        }

        public async Task<bool> WriteDeviceAsync(DeviceOperation op, CancellationToken cancellationToken) {
            return await Task.Run(() => WriteDevice(op, cancellationToken), cancellationToken);
        }

        public async Task<bool> VerifyDeviceAsync(DeviceOperation op, CancellationToken cancellationToken) {
            return await Task.Run(() => VerifyDevice(op, cancellationToken), cancellationToken);
        }

        public async Task<bool> EraseDeviceAsync(DeviceOperation op, CancellationToken cancellationToken) {
            return await Task.Run(() => EraseDevice(op, cancellationToken), cancellationToken);
        }

        public void SaveFile(string fileName) {
            var hfb = new HexFileBuilder();
            foreach (var sourceLine in FlashHexBoard.Lines) {
                hfb.SetAddress(sourceLine.Address);
                foreach (var bt in sourceLine.Bytes) {
                    hfb.WriteByte(bt.Value);
                }
            }
            var hf = hfb.Build();
            hf.Save(fileName);
        }

        private static IProgrammer CreateProgrammer(DeviceOperation progress, CancellationToken cancellationToken) {
            var settings = FlasherConfig.Read();
            var inner = CreateProgrammerFromConfig(settings);
            var programmer = new ProgressTrackerProgrammer(inner, progress, cancellationToken);
            return programmer;
        }

        private static IProgrammer CreateProgrammerFromConfig(FlasherConfig settings) {
            switch (settings.ProgrammerType) {
                case ProgrammerType.AvrIsp:
                    return settings.AvrIsp.CreateProgrammer();
                case ProgrammerType.ComBitBang:
                    return settings.ComBitBang.CreateProgrammer();
                case ProgrammerType.Stub:
                    return new StubProgrammer();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
