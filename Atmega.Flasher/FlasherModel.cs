using System.ComponentModel;
using System.Runtime.CompilerServices;
using Atmega.Flasher.AvrIsp;
using Atmega.Flasher.Hex;
using Atmega.Flasher.Models;
using Atmega.Hex;

namespace Atmega.Flasher {
    public class FlasherModel : INotifyPropertyChanged {

        private HexBoard _eepromHexBoard = new HexBoard();
        private HexBoard _flashHexBoard = new HexBoard();
        private readonly FlasherConfig _settings = new FlasherConfig();

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

        public void ReadDevice() {

            using (var programmer = CreateProgrammer()) {
                programmer.Start();

                EepromHexBoard = ReadMemory(programmer, AvrMemoryType.Eeprom, EepromSize);
                FlashHexBoard = ReadMemory(programmer, AvrMemoryType.Flash, FlashSize);

                programmer.Stop();
            }
        }

        private static HexBoard ReadMemory(IProgrammer programmer, AvrMemoryType memType, int size) {
            var board = new HexBoard();

            var data = programmer.ReadPage(0, size, memType);
            var offset = 0;

            foreach (var bt in data) {
                board[offset] = bt;
                offset++;
            }

            return board;
        }

        public int EepromSize {
            get { return 1024; }
        }

        public int FlashSize {
            get { return 32768; }
        }

        public FlasherConfig Settings {
            get { return _settings; }
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

        private IProgrammer CreateProgrammer() {
            return new AvrIspProgrammer(new AvrIspClient("COM4"));
        }
    }
}
