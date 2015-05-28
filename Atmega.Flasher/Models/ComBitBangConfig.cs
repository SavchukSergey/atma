using System.Collections.ObjectModel;

namespace Atmega.Flasher.Models {
    public class ComBitBangConfig : BaseConfig {

        private readonly ComPortSettings _comPortSettings;
        private readonly ComBitBangPinConfig _resetPin;
        private readonly ComBitBangPinConfig _clkPin;
        private readonly ComBitBangPinConfig _mosiPin;
        private readonly ComBitBangPinConfig _misoPin;

        private readonly ObservableCollection<string> _inputComPins = new ObservableCollection<string>();
        private readonly ObservableCollection<string> _outputComPins = new ObservableCollection<string>();

        public ComBitBangConfig(string keyPrefix)
            : base(keyPrefix) {
            _comPortSettings = new ComPortSettings(keyPrefix);
            _resetPin = new ComBitBangPinConfig(keyPrefix + "ResetPin.");
            _clkPin = new ComBitBangPinConfig(keyPrefix + "ClkPin.");
            _mosiPin = new ComBitBangPinConfig(keyPrefix + "MosiPin.");
            _misoPin = new ComBitBangPinConfig(keyPrefix + "MisoPin.");

            _inputComPins.Add("CTS");
            _inputComPins.Add("CD");
            _inputComPins.Add("DSR");
            _inputComPins.Add("None");

            _outputComPins.Add("RTS");
            _outputComPins.Add("DTR");
            _outputComPins.Add("None");
        }

        public ComBitBangPinConfig ResetPin { get { return _resetPin; } }
        
        public ComBitBangPinConfig ClkPin { get { return _clkPin; } }

        public ComBitBangPinConfig MosiPin { get { return _mosiPin; } }
        
        public ComBitBangPinConfig MisoPin { get { return _misoPin; } }


        public ObservableCollection<string> InputComPins {
            get { return _inputComPins; }
        }

        public ObservableCollection<string> OuputComPins {
            get { return _outputComPins; }
        }

        public ComPortSettings ComPortSettings {
            get { return _comPortSettings; }
        }

        public override void Save() {
            _comPortSettings.Save();
            _resetPin.Save();
            _clkPin.Save();
            _mosiPin.Save();
            _misoPin.Save();
        }

        public override void ReadFromConfig() {
            _comPortSettings.ReadFromConfig();
            _resetPin.ReadFromConfig();
            _clkPin.ReadFromConfig();
            _mosiPin.ReadFromConfig();
            _misoPin.ReadFromConfig();
        }
    }
}
