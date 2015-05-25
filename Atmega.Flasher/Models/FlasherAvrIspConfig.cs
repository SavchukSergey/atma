using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using Atmega.Flasher.Annotations;

namespace Atmega.Flasher.Models {
    public class FlasherAvrIspConfig : INotifyPropertyChanged {

        private readonly ObservableCollection<string> _comPorts = new ObservableCollection<string>();
        private readonly ObservableCollection<int> _baudRates = new ObservableCollection<int>();

        public string ComPort {
            get { return GetConfigString("COM1"); }
            set {
                var old = ComPort;
                if (old != value) {
                    OnPropertyChanged();
                    UpdateConfig(value);
                }
            }
        }

        public int BaudRate {
            get {
                return GetConfigInt(57600);
            }
            set {
                var old = BaudRate;
                if (old != value) {
                    OnPropertyChanged();
                    UpdateConfig(value.ToString());
                }
            }
        }

        public void Reload() {
            _comPorts.Clear();
            foreach (var port in SerialPort.GetPortNames()) {
                _comPorts.Add(port);
            }
            if (_comPorts.Count == 0) {
                _comPorts.Add("COM1");
            }

            _baudRates.Clear();
            _baudRates.Add(9600);
            _baudRates.Add(19200);
            _baudRates.Add(57600);
        }

        public ObservableCollection<string> ComPorts {
            get {
                return _comPorts;
            }
        }

        public ObservableCollection<int> BaudRates {
            get {
                return _baudRates;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string GetConfig(string key) {
            key = "AvrIsp." + key;
            return ConfigurationManager.AppSettings[key];
        }

        private string GetConfigString(string defaultValue, [CallerMemberName] string key = null) {
            return GetConfig(key) ?? defaultValue;
        }

        private int GetConfigInt(int defaultValue, [CallerMemberName] string key = null) {
            var raw = GetConfig(key);
            int res;
            if (int.TryParse(raw, out res)) return res;
            return defaultValue;
        }

        private void UpdateConfig(string value, [CallerMemberName] string key = null) {
            key = "AvrIsp." + key;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var set = config.AppSettings.Settings[key];
            if (set != null) {
                config.AppSettings.Settings[key].Value = value;
            } else {
                config.AppSettings.Settings.Add(new KeyValueConfigurationElement(key, value));
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
