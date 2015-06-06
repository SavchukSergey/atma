using System;
using System.IO.Ports;

namespace Atmega.Flasher.IO {
    public class SerialPortChannel : IAvrChannel {

        private readonly SerialPort _port;

        public SerialPortChannel(SerialPort port) {
            _port = port;
            port.ReadTimeout = 500;
        }

        public void Dispose() {
            _port.Dispose();
        }

        public void Open() {
            _port.Open();
        }

        public void Close() {
            _port.Close();
        }

        public void ToggleReset(bool val) {
            _port.DtrEnable = val;
        }

        public void SendByte(byte bt) {
            _port.Write(new[] { bt }, 0, 1);
            Console.Write("{1} [{0:x2}] ", bt, (bt >= 32 && bt <= 127) ? (char)bt : '.');
        }

        public byte ReceiveByte() {
            return (byte)_port.ReadByte();
        }
    }
}
