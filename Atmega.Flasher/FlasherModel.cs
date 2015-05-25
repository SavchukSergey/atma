using System;
using System.ComponentModel;
using System.Text;
using System.Threading;
using Atmega.Asm.Hex;
using Atmega.Flasher.AvrIsp;
using Atmega.Flasher.Hex;
using Atmega.Hex;

namespace Atmega.Flasher {
    public class FlasherModel : INotifyPropertyChanged {

        private HexBoard _hexFile = new HexBoard();

        public FlasherModel() {
            HexBoard[0] = null;
        }

        public void OpenFile(string filePath) {
            var hexFile = HexFile.Load(filePath);
            HexBoard = HexBoard.From(hexFile);
        }

        public HexBoard HexBoard {
            get {
                return _hexFile;
            }
            set {
                _hexFile = value;
                OnPropertyChanged("HexFormatted");
            }
        }

        public string HexFormatted {
            get {
                var sb = new StringBuilder();
                foreach (var line in _hexFile.Lines) {
                    sb.AppendFormat("{0:x4}: ", line.Address);
                    foreach (var bt in line.Bytes) {
                        var val = bt.Value;
                        if (val.HasValue) {
                            sb.AppendFormat("{0:x2} ", val);
                        } else {
                            sb.Append("-- ");
                        }
                    }
                    foreach (var bt in line.Bytes) {
                        var val = bt.Value;
                        if (val.HasValue && val.Value >= 32 && val.Value < 128) {
                            var ch = (char)val;
                            sb.Append(ch);
                        } else {
                            sb.Append(" ");
                        }
                    }
                    sb.AppendLine();
                }
                return sb.ToString();
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

                var board = new HexBoard();

                var offset = 0;
                var size = FlashSize;
                var blockSize = 256;
                while (offset < size) {
                    port.SetAddress((ushort)(offset >> 1));
                    var cnt = Math.Min(size - offset, blockSize);

                    var data = port.ReadMemory(cnt);

                    foreach (var bt in data) {
                        board[offset] = bt;
                        offset++;
                    }
                }

                HexBoard = board;

                port.EndProgram();

                port.Close();
            }
        }

        public int FlashSize {
            get { return 32768; }
        }
    }
}
