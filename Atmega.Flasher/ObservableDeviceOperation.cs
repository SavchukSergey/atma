using System.ComponentModel;
using System.Runtime.CompilerServices;
using Atmega.Flasher.Annotations;

namespace Atmega.Flasher {
    public class ObservableDeviceOperation : DeviceOperation, INotifyPropertyChanged {

        private int _eepromDone;
        private int _flashSize;
        private int _eepromSize;
        private int _flashDone;

        public override int EepromDone {
            get { return _eepromDone; }
            set {
                if (value != _eepromDone) {
                    _eepromDone = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Progress");
                }
            }
        }

        public override int FlashDone {
            get { return _flashDone; }
            set {
                if (value != _flashDone) {
                    _flashDone = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Progress");
                }
            }
        }

        public override int FlashSize {
            get { return _flashSize; }
            set {
                if (value != _flashSize) {
                    _flashSize = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Progress");
                }
            }
        }

        public override int EepromSize {
            get { return _eepromSize; }
            set {
                if (value != _eepromSize) {
                    _eepromSize = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Progress");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
