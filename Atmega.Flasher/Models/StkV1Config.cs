using Atmega.Flasher.IO;
using Atmega.Flasher.STKv1;

namespace Atmega.Flasher.Models {
    public class StkV1Config : BaseProgrammerConfig {

        private readonly ComPortSettings _comPortSettings;

        public StkV1Config(string keyPrefix)
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

        public override IProgrammer CreateProgrammer(DeviceInfo device) {
            var port = ComPortSettings.CreateSerialPort();
            return new StkV1Programmer(new StkV1Client(new SerialPortChannel(port)), device);
        }
    }
}
