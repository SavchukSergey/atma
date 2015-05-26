using System.Collections.ObjectModel;
using System.IO.Ports;

namespace Atmega.Flasher.Models {
    public class FlasherAvrIspConfig : BaseConfig {

        private string _comPort;
        private int _baudRate;
        private readonly ObservableCollection<string> _comPorts = new ObservableCollection<string>();
        private readonly ObservableCollection<int> _baudRates = new ObservableCollection<int>();

        public string ComPort {
            get { return _comPort; }
            set {
                if (_comPort != value) {
                    _comPort = value;
                    OnPropertyChanged();
                }
            }
        }

        public int BaudRate {
            get {
                return _baudRate;
            }
            set {
                if (_baudRate != value) {
                    _baudRate = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> ComPorts { get { return _comPorts; } }

        public ObservableCollection<int> BaudRates { get { return _baudRates; } }

        public static FlasherAvrIspConfig ReadFromConfig() {
            var res = new FlasherAvrIspConfig();
            res.ComPort = res.GetConfigString("COM1", "ComPort");
            res.BaudRate = res.GetConfigInt(57600, "BaudRate");

            res.ComPorts.Clear();
            foreach (var port in SerialPort.GetPortNames()) {
                res.ComPorts.Add(port);
            }
            if (res.ComPorts.Count == 0) {
                res.ComPorts.Add("COM1");
            }

            res.BaudRates.Clear();
            res.BaudRates.Add(9600);
            res.BaudRates.Add(19200);
            res.BaudRates.Add(57600);

            return res;
        }

        public override void Save() {
            UpdateConfig(ComPort, "ComPort");
            UpdateConfig(BaudRate.ToString(), "BaudRate");
        }

        protected override string KeyPrefix {
            get { return "AvrIsp."; }
        }
    }
}
