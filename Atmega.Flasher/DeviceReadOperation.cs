using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using Atmega.Flasher.Annotations;

namespace Atmega.Flasher {
    public class DeviceReadOperation : INotifyPropertyChanged {

        private readonly Dispatcher _dispatcher;
        private int _done;
        private int _total;
        private int _eepromDone;
        private int _flashSize;
        private int _eepromSize;
        private int _flashDone;

        public DeviceReadOperation(Dispatcher dispatcher) {
            _dispatcher = dispatcher;
        }

        public int Done {
            get { return _flashDone + _eepromDone; }
        }

        public int Total {
            get { return _flashSize + _eepromSize; }
        }

        public double Progress {
            get { return 100.0 * Done / Math.Max(1, Total); }
        }

        public int EepromDone {
            get { return _eepromDone; }
            set {
                if (value != _eepromDone) {
                    _eepromDone = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Progress");
                }
            }
        }

        public int FlashDone {
            get { return _flashDone; }
            set {
                if (value != _flashDone) {
                    _flashDone = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Progress");
                }
            }
        }

        public int FlashSize {
            get { return _flashSize; }
            set {
                if (value != _flashSize) {
                    _flashSize = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Progress");
                }
            }
        }

        public int EepromSize {
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
                _dispatcher.Invoke(() => handler(this, new PropertyChangedEventArgs(propertyName)));
            }
        }
    }
}
