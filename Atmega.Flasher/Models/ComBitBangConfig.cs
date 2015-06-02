using System.Collections.ObjectModel;
using Atmega.Flasher.AvrSpi;
using Atmega.Flasher.IO;

namespace Atmega.Flasher.Models {
    public class ComBitBangConfig : BaseProgrammerConfig {

        private readonly ComPortSettings _comPortSettings;
        private readonly ComBitBangPinConfig _resetPin;
        private readonly ComBitBangPinConfig _clkPin;
        private readonly ComBitBangPinConfig _mosiPin;
        private readonly ComBitBangPinConfig _misoPin;

        private readonly ObservableCollection<ComPinType> _inputComPins = new ObservableCollection<ComPinType>();
        private readonly ObservableCollection<ComPinType> _outputComPins = new ObservableCollection<ComPinType>();

        public ComBitBangConfig(string keyPrefix)
            : base(keyPrefix) {
            _comPortSettings = new ComPortSettings(keyPrefix);
            _resetPin = new ComBitBangPinConfig(keyPrefix + "ResetPin.");
            _clkPin = new ComBitBangPinConfig(keyPrefix + "ClkPin.");
            _mosiPin = new ComBitBangPinConfig(keyPrefix + "MosiPin.");
            _misoPin = new ComBitBangPinConfig(keyPrefix + "MisoPin.");

            _inputComPins.Add(ComPinType.Cts);
            _inputComPins.Add(ComPinType.CD);
            _inputComPins.Add(ComPinType.Dsr);
            _inputComPins.Add(ComPinType.None);

            _outputComPins.Add(ComPinType.Rts);
            _outputComPins.Add(ComPinType.Dtr);
            _outputComPins.Add(ComPinType.TxD);
            _outputComPins.Add(ComPinType.None);
        }

        public ComBitBangPinConfig ResetPin { get { return _resetPin; } }

        public ComBitBangPinConfig ClkPin { get { return _clkPin; } }

        public ComBitBangPinConfig MosiPin { get { return _mosiPin; } }

        public ComBitBangPinConfig MisoPin { get { return _misoPin; } }


        public ObservableCollection<ComPinType> InputComPins {
            get { return _inputComPins; }
        }

        public ObservableCollection<ComPinType> OuputComPins {
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

        public override IProgrammer CreateProgrammer() {
            var port = ComPortSettings.CreateSerialPort();
            var spiMaster = new SpiMaster(port, ClkPin.CreatePin(port), MosiPin.CreatePin(port), MisoPin.CreatePin(port));
            return new AvrSpiProgrammer(new AvrSpiClient(spiMaster));
        }
    }
}
