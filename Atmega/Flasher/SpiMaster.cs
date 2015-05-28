using System;
using System.IO.Ports;

namespace Atmega.Flasher {
    public class SpiMaster : IDisposable {
        private readonly SerialPort _port;
        private readonly ComPin _clkPin;

        public SpiMaster(SerialPort port, ComPin clkPin) {
            _port = port;
            _clkPin = clkPin;
        }

        public void Open() {
            _port.Open();
        }

        public void Close() {
            _port.Close();
        }

        public byte SendByte(byte val) {
            var reply = 0;
            for (var i = 0; i < 8; i++) {
                var replyBit = SendBit((val & 0x80) > 0);
                reply <<= 1;
                reply += replyBit ? 1 : 0;
                val <<= 1;
            }
            return (byte)reply;
        }

        public bool SendBit(bool val) {
            SetMosi(val);
            SetClock();
            var res = GetMiso();
            ResetClock();
            return res;
        }

        public bool GetMiso() {
            return _port.DsrHolding;
        }

        public void SetMosi(bool val) {
            _port.DtrEnable = val;
        }

        public void SetClock() {
            _clkPin.Set();
            DelayHigh();
        }

        public void ResetClock() {
            _clkPin.Reset();
            DelayLow();
        }


        private void DelayLow() {
            for (var i = 0; i < 100; i++) {
            }
        }

        private void DelayHigh() {
            DelayLow();
            DelayLow();
            DelayLow();
            DelayLow();
        }

        public void Dispose() {
            _port.Dispose();
        }
    }
}
