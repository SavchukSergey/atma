using System;
using System.Globalization;
using System.IO.Ports;

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

        public ComPin CreatePin(SerialPort port) {
            return new ComPin(port, GetPinType(), Invert);
        }

        private ComPinType GetPinType() {
            switch (Pin.ToLowerInvariant()) {
                case "rts":
                    return ComPinType.Rts;
                case "dtr":
                    return ComPinType.Dtr;
                case "cts":
                    return ComPinType.Cts;
                case "cd":
                    return ComPinType.CD;
                case "dsr":
                    return ComPinType.Dsr;
                case "none":
                    return ComPinType.None;
                default:
                    throw new NotSupportedException();
            }
        }

    }
}
