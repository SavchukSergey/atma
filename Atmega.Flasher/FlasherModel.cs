using System.ComponentModel;
using System.Text;
using Atmega.Asm.Hex;

namespace Atmega.Flasher {
    public class FlasherModel : INotifyPropertyChanged {

        private HexFile _hexFile = new HexFile();

        public void OpenFile(string filePath) {
            _hexFile = HexFile.Load(filePath);
            OnPropertyChanged("HexFormatted");
        }

        public HexFile HexFile {
            get {
                return _hexFile;
            }
        }

        public string HexFormatted {
            get {
                var sb = new StringBuilder();
                foreach (var line in _hexFile.Lines) {
                    sb.AppendFormat("{0:x4}:", line.Address);
                    foreach (var bt in line.Data) {
                        sb.AppendFormat("{0:x2} ", bt);
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
