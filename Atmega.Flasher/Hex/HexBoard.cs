using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Atmega.Asm.Hex;
using Atmega.Hex;

namespace Atmega.Flasher.Hex {
    public class HexBoard : INotifyPropertyChanged {

        private readonly ObservableCollection<HexBoardLine> _lines = new ObservableCollection<HexBoardLine>();

        private HexBoardLine EnsureLine(int adr) {
            adr = (adr >> 4) << 4;
            var line = _lines.FirstOrDefault(item => item.Address == adr);
            if (line == null) {
                var i = 0;
                while (i < _lines.Count && _lines[i].Address < adr)
                    i++;

                line = new HexBoardLine {
                    Address = adr
                };
                _lines.Insert(i, line);
            }
            return line;
        }

        public byte? this[int address] {
            get { return null; }
            set {
                var line = EnsureLine(address);
                line.Bytes[address % 16] = value;
                OnPropertyChanged("HexFormatted");
            }
        }

        public ObservableCollection<HexBoardLine> Lines { get { return _lines; } }

        public static HexBoard From(HexFile file) {
            var res = new HexBoard();
            foreach (var line in file.Lines) {
                for (int index = 0; index < line.Data.Length; index++) {
                    var bt = line.Data[index];
                    res[line.Address + index] = bt;
                }
            }
            return res;
        }


        public string HexFormatted {
            get {
                var sb = new StringBuilder();
                foreach (var line in _lines) {
                    sb.AppendFormat("{0:x4}: ", line.Address);
                    foreach (var bt in line.Bytes) {
                        var val = bt.Value;
                        if (val.HasValue) {
                            sb.AppendFormat("{0:x2} ", val);
                        } else {
                            sb.Append("-- ");
                        }
                    }
                    sb.Append("  |  ");
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
    }
}
