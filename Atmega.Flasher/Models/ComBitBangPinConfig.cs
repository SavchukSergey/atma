using System;
using System.Globalization;

namespace Atmega.Flasher.Models {
    public class ComBitBangPinConfig : BaseConfig {
        private string _pin;
        private bool _invert;

        public ComBitBangPinConfig(string keyPrefix)
            : base(keyPrefix) {
        }

        public string Pin {
            get { return _pin; }
            set {
                if (value != _pin) {
                    _pin = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Invert {
            get { return _invert; }
            set {
                if (value != _invert) {
                    _invert = value;
                    OnPropertyChanged();
                }
            }
        }

        public override void Save() {
            UpdateConfig(Pin, "Pin");
            UpdateConfigBool("Invert", Invert);
        }

        public override void ReadFromConfig() {
            Pin = GetConfigString("None", "Pin");
            Invert = GetConfigBool(false, "Invert");
        }

    }
}
