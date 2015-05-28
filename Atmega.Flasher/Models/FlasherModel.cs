using System;
using System.ComponentModel;
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
            try {
                var config = FlasherConfig.Read();
                var device = config.Device;
                var flashSize = device.FlashSize;
                var eepromSize = device.EepromSize;
                op.FlashSize += flashSize;
                op.EepromSize += eepromSize;

                byte[] eepData;
                byte[] flashData;
                using (var programmer = CreateProgrammer(op, cancellationToken)) {
                    programmer.Start();
                    flashData = programmer.ReadPage(0, flashSize, AvrMemoryType.Flash);
                    eepData = programmer.ReadPage(0, eepromSize, AvrMemoryType.Eeprom);
                    programmer.Stop();
                }
                op.CurrentState = "Everything is done";

                EepromHexBoard = HexBoard.From(eepData);
                FlashHexBoard = HexBoard.From(flashData);

                return true;
            } catch (TimeoutException) {
                op.CurrentState = "Device is not ready";
                return false;
            }
        }

        public bool WriteDevice(DeviceOperation op, CancellationToken cancellationToken) {
            try {
                var eepromBlocks = EepromHexBoard.SplitBlocks();
                var flashBlocks = FlashHexBoard.SplitBlocks();

                op.FlashSize += flashBlocks.TotalBytes;
                op.EepromSize += eepromBlocks.TotalBytes;

                using (var programmer = CreateProgrammer(op, cancellationToken)) {
                    programmer.Start();

                    foreach (var block in flashBlocks.Blocks) {
                        programmer.WritePage(block.Address, AvrMemoryType.Flash, block.Data);
                    }

                    foreach (var block in eepromBlocks.Blocks) {
                        programmer.WritePage(block.Address, AvrMemoryType.Eeprom, block.Data);
                    }

                    programmer.Stop();
                }

                return true;
            } catch (Exception) {
                op.CurrentState = "Device is not ready";
                return false;
            }
        }

        public async Task<bool> ReadDeviceAsync(DeviceOperation op, CancellationToken cancellationToken) {
            return await Task.Run(() => ReadDevice(op, cancellationToken), cancellationToken);
        }

        public async Task<bool> WriteDeviceAsync(DeviceOperation op, CancellationToken cancellationToken) {
            return await Task.Run(() => WriteDevice(op, cancellationToken), cancellationToken);
        }

        //public int EepromSize {
        //    get { return 1024; }
        //}

        //public int FlashSize {
        //    get { return 32768; }
        //}

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
