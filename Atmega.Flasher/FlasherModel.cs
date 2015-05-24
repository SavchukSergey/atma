using System.ComponentModel;
using System.Text;
using Atmega.Asm.Hex;
using Atmega.Flasher.Hex;
using Atmega.Hex;

namespace Atmega.Flasher {
    public class FlasherModel : INotifyPropertyChanged {

        private HexBoard _hexFile = new HexBoard();

        public void OpenFile(string filePath) {
            var hexFile = HexFile.Load(filePath);
            _hexFile = HexBoard.From(hexFile);
            OnPropertyChanged("HexFormatted");
        }

        public HexBoard HexBoard {
            get {
                return _hexFile;
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
    }
}
