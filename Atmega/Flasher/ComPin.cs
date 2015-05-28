﻿using System;
using System.IO.Ports;

namespace Atmega.Flasher {
    public class ComPin {
        private readonly SerialPort _port;
        private readonly ComPinType _pin;
        private readonly bool _invert;

        public ComPin(SerialPort port, ComPinType pin, bool invert) {
            _port = port;
            _pin = pin;
            _invert = invert;
        }

        public void Set() {
            Set(true);
        }

        public void Reset() {
            Set(false);
        }

        public bool Get() {
            switch (_pin) {
                case ComPinType.Cts:
                    return _port.CtsHolding ^ _invert;
                case ComPinType.CD:
                    return _port.CDHolding ^ _invert;
                case ComPinType.Dsr:
                    return _port.DsrHolding ^ _invert;
                case ComPinType.None:
                    return _invert;
                default:
                    throw new NotSupportedException();
            }
        }

        public void Set(bool value) {
            switch (_pin) {
                case ComPinType.Rts:
                    _port.RtsEnable = value ^ _invert;
                    break;
                case ComPinType.Dtr:
                    _port.DtrEnable = value ^ _invert;
                    break;
            }
        }
    }
}
