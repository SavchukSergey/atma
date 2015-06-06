using Atmega.Flasher.IO;
using Atmega.Flasher.STKv1;

namespace Atmega.Flasher.Models {
    public class StkV1Config : BaseProgrammerConfig {

        private readonly ComPortSettings _comPortSettings;
        private readonly ComBitBangPinConfig _resetPin;

        public StkV1Config(string keyPrefix)
            : base(keyPrefix) {
            _comPortSettings = new ComPortSettings(keyPrefix);
            _resetPin = new ComBitBangPinConfig(keyPrefix + "ResetPin.");
        }

        public ComPortSettings ComPortSettings {
            get { return _comPortSettings; }
        }

        public ComBitBangPinConfig ResetPin { get { return _resetPin; } }

        public override void Save() {
            _comPortSettings.Save();
            _resetPin.Save();
        }

        public override void ReadFromConfig() {
            _comPortSettings.ReadFromConfig();
            _resetPin.ReadFromConfig();
        }

        public override IProgrammer CreateProgrammer(DeviceInfo device) {
            var port = ComPortSettings.CreateSerialPort();
            return new StkV1Programmer(new StkV1Client(new SerialPortChannel(port, ResetPin.CreatePin(port))), device);
        }
    }
}
