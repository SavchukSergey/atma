using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Atmega.Flasher.AvrIsp {
    public class AvrIspClient : IDisposable {

        private const byte STK_INSYNC = 0x14;
        private const byte STK_OK = 0x10;
        private const byte CRC_EOP = 0x20;

        private readonly SerialPort _port;

        private ushort _position;

        public AvrIspClient(SerialPort port) {
            _port = port;
            port.ReadTimeout = 500;
        }

        public void ResetDevice() {
            _port.DtrEnable = true;
            Thread.Sleep(200);
            _port.DtrEnable = false;
            Thread.Sleep(200);
            _port.DtrEnable = true;
            Thread.Sleep(200);
            //_port.DtrEnable = true; //in-circuit capacity will break dtr signal. so we can skip dtr=true
        }

        public void Open() {
            _port.Open();
        }

        public void StartProgram() {
            WriteChar('P');
            Thread.Sleep(200);
            ReadEmpty();
        }

        public void EndProgram() {
            WriteChar('Q');
            ReadEmpty();
        }

        public byte Universal(byte a, byte b, byte c, byte d) {
            WriteChar('V');
            WriteByte(a);
            WriteByte(b);
            WriteByte(c);
            WriteByte(d);
            return BRead();
        }

        public string GetProgrammerId() {
            WriteChar('1');
            WriteByte(CRC_EOP);
            var sb = new StringBuilder();
            ReadAssert(STK_INSYNC);
            while (true) {
                var bt = ReadByte();
                if (bt == STK_OK) break;
                sb.Append((char)bt);
            }
            return sb.ToString();
        }

        public void SetAddress(ushort val) {
            WriteChar('U');
            for (var i = 0; i < 2; i++) {
                var bt = (byte)(val & 0xff);
                WriteByte(bt);
                val >>= 8;
            }
            ReadEmpty();
        }

        //todo: when reading flash. response is always in words. You request three byte - you get 4 byte response. low byte first
        public byte[] ReadPage(int length, AvrMemoryType memType) {
            WriteChar('t');
            WriteByte((byte)(length >> 8));
            WriteByte((byte)(length & 0xff));
            switch (memType) {
                case AvrMemoryType.Flash:
                    WriteChar('F');
                    break;
                case AvrMemoryType.Eeprom:
                    WriteChar('E');
                    break;
                default:
                    throw new Exception();
            }
            WriteByte(CRC_EOP);

            ReadAssert(STK_INSYNC);

            var res = new byte[length];
            for (var i = 0; i < length; i++) {
                res[i] = ReadByte();
            }

            ReadAssert(STK_OK);

            return res;
        }

        public byte[] ReadFlash(int length) {
            return ReadPage(length, AvrMemoryType.Flash);
        }

        public byte[] ReadEeprom(int length) {
            return ReadPage(length, AvrMemoryType.Eeprom);
        }

        public void WriteFlash(byte[] data, int offset, int length) {
            WriteChar('d');
            WriteByte((byte)(length >> 8));
            WriteByte((byte)(length & 0xff));
            WriteChar('F');

            for (var i = 0; i < length; i++) {
                var bt = data[offset + i];
                WriteByte(bt);
            }

            WriteByte(CRC_EOP);

            ReadEmpty();
        }

        public void WriteEeprom(byte[] data, int offset, int length) {
            WriteChar('d');
            WriteByte((byte)(length >> 8));
            WriteByte((byte)(length & 0xff));
            WriteChar('E');

            for (var i = 0; i < length; i++) {
                var bt = data[offset + i];
                WriteByte(bt);
            }

            WriteByte(CRC_EOP);

            ReadEmpty();
        }

        private byte BRead() {
            WriteByte(CRC_EOP);
            ReadAssert(STK_INSYNC);
            var res = ReadByte();
            ReadAssert(STK_OK);
            return res;
        }

        public byte GetVersion(byte arg) {
            WriteChar('A');
            WriteByte(arg);
            return BRead();
        }

        public AvrIspVersion ReadVersion() {
            return new AvrIspVersion {
                Hardware = GetVersion(0x80),
                Major = GetVersion(0x81),
                Minor = GetVersion(0x82),
                Type = (char)GetVersion(0x93)
            };
        }

        public void Close() {
            _port.Close();
        }

        private void ReadEmpty() {
            WriteByte(CRC_EOP);
            ReadAssert(STK_INSYNC);
            ReadAssert(STK_OK);
        }

        private byte ReadByte() {
            return (byte)_port.ReadByte();
        }

        private void WriteChar(char ch) {
            WriteByte((byte)ch);
        }

        private void WriteByte(byte bt) {
            _port.Write(new[] { bt }, 0, 1);
        }

        public void Dispose() {
            _port.Dispose();
        }

        private void ReadAssert(byte bt) {
            var res = _port.ReadByte();
            if (res != bt) throw new Exception("nosync");
        }

    }
}
