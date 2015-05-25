using System;
using System.ComponentModel;
using Atmega.Flasher.AvrIsp;
using Atmega.Flasher.Hex;
using Atmega.Hex;

namespace Atmega.Flasher {
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
                OnPropertyChanged("EepromHexBoard");
            }
        }

        public HexBoard FlashHexBoard {
            get {
                return _flashHexBoard;
            }
            set {
                _flashHexBoard = value;
                OnPropertyChanged("FlashHexBoard");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ReadDevice() {
            using (var port = new AvrIspClient("COM4")) {
                port.Open();
                port.ResetDevice();
                port.StartProgram();

                var progId = port.GetProgrammerId();

                //var version = port.ReadVersion();
                ////Console.WriteLine("Version: {0:x2}.{1:x2}.{2:x2}", version.Hardware, version.Major, version.Minor);

                ReadEeprom(port);
                ReadFlash(port);

                port.EndProgram();

                port.Close();
            }
        }

        private void ReadEeprom(AvrIspClient port) {
            var board = new HexBoard();

            var offset = 0;
            var size = EepromSize;
            var blockSize = 256;
            while (offset < size) {
                port.SetAddress((ushort)(offset >> 1));
                var cnt = Math.Min(size - offset, blockSize);

                var data = port.ReadEeprom(cnt);

                foreach (var bt in data) {
                    board[offset] = bt;
                    offset++;
                }
            }

            EepromHexBoard = board;
        }

        private void ReadFlash(AvrIspClient port) {
            var board = new HexBoard();

            var offset = 0;
            var size = FlashSize;
            var blockSize = 256;
            while (offset < size) {
                port.SetAddress((ushort)(offset >> 1));
                var cnt = Math.Min(size - offset, blockSize);

                var data = port.ReadFlash(cnt);

                foreach (var bt in data) {
                    board[offset] = bt;
                    offset++;
                }
            }

            FlashHexBoard = board;
        }

        public int EepromSize {
            get { return 1024; }
        }

        public int FlashSize {
            get { return 32768; }
        }
    }
}
