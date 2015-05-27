namespace Atmega.Flasher.Models {
    public class AvrIspConfig : BaseConfig {

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

    }
}
