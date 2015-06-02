using Atmega.Flasher.AvrIsp;
using Atmega.Flasher.IO;

namespace Atmega.Flasher.Models {
    public class AvrIspConfig : BaseProgrammerConfig {

        private readonly ComPortSettings _comPortSettings;

        public AvrIspConfig(string keyPrefix)
            : base(keyPrefix) {
            _comPortSettings = new ComPortSettings(keyPrefix);
        }

        public ComPortSettings ComPortSettings {
            get { return _comPortSettings; }
        }

        public override void Save() {
            _comPortSettings.Save();
        }

        public override void ReadFromConfig() {
            _comPortSettings.ReadFromConfig();
        }

        public override IProgrammer CreateProgrammer() {
            var port = ComPortSettings.CreateSerialPort();
            return new AvrIspProgrammer(new AvrIspClient(new SerialPortChannel(port)));
        }
    }
}
