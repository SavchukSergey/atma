using System;
using System.IO.Ports;
using System.Threading;

namespace Atmega.Flasher.AvrSpi {
    public class AvrSpiClient : IDisposable {

        private readonly SerialPort _port;

        public AvrSpiClient(SerialPort port) {
            _port = port;
        }

        public AvrSpiClient(string portName)
            : this(new SerialPort(portName) {
                BaudRate = 19200 / 2,
            }) {
        }

        public void Start() {
            ResetPin = true;
            ResetClock();
            Thread.Sleep(50);
            ResetPin = false;
            Thread.Sleep(50);
            SpiTransaction(0xAC, 0x53, 0x00, 0x00);
        }

        public void Stop() {
            ResetPin = true;
        }

        public byte SpiTransaction(byte a, byte b, byte c, byte d) {
            SendByte(a);
            SendByte(b);
            SendByte(c);
            return SendByte(d);
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

        private void Delay() {
            for (var i = 0; i < 100; i++) {
            }
        }

        private bool SendBit(bool val) {
            SetMosi(val);
            SetClock();
            Delay();
            var res = GetMiso();
            ResetClock();
            return res;
        }

        private bool GetMiso() {
            return _port.DsrHolding;
        }

        private void SetMosi(bool val) {
            _port.DtrEnable = val;
        }

        private void SetClock() {
            _port.RtsEnable = true;
        }

        private void ResetClock() {
            _port.RtsEnable = false;
        }

        private bool ResetPin {
            get { return false; }
            set { }
        }

        public void Dispose() {
            _port.Dispose();
        }
    }
}
