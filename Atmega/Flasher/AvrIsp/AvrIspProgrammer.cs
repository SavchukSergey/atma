using System;
using Atmega.Flasher.STKv1;

namespace Atmega.Flasher.AvrIsp {
    public class AvrIspProgrammer : IProgrammer {

        private readonly StkV1Client _client;
        private const int BLOCK_SIZE = 1024;

        public AvrIspProgrammer(StkV1Client client) {
            _client = client;
        }

        public void Start() {
            _client.Open();
            _client.ResetDevice();
            _client.EnterProgramMode();
        }

        public void Stop() {
            _client.LeaveProgramMode();
            _client.Close();
        }

        public void ReadPage(int address, AvrMemoryType memType, byte[] data, int dataStart, int dataLength) {
            switch (memType) {
                case AvrMemoryType.Eeprom:
                    ReadEeprom(address, data, dataStart, dataLength);
                    break;
                case AvrMemoryType.Flash:
                    ReadFlash(address, data, dataStart, dataLength);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public void WritePage(int address, AvrMemoryType memType, byte[] data, int dataStart, int dataLength) {
            switch (memType) {
                case AvrMemoryType.Eeprom:
                    WriteEeprom(address, data, dataStart, dataLength);
                    break;
                case AvrMemoryType.Flash:
                    WriteFlash(address, data, dataStart, dataLength);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public AtmegaLockBits ReadLockBits() {
            return new AtmegaLockBits { Value = _client.Universal(0x58, 0x00, 0x00, 0x00) };
        }

        public void WriteLockBits(AtmegaLockBits bits) {
            _client.Universal(0xac, 0xe0, 0x00, bits.Value);
        }

        public void EraseDevice() {
            _client.Universal(0xac, 0x80, 0x00, 0x00);
        }

        private void WriteEeprom(int address, byte[] data, int dataStart, int dataLength) {
            var offset = address;
            var end = address + dataLength;
            while (offset < end) {
                _client.LoadAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);

                _client.ProgramPage(data, offset - address + dataStart, cnt, AvrMemoryType.Eeprom);
                offset += cnt;
            }
        }

        private void ReadEeprom(int address, byte[] data, int dataStart, int dataLength) {
            var offset = address;
            var end = address + dataLength;
            while (offset < end) {
                _client.LoadAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);
                _client.ReadPage(data, offset - address + dataStart, cnt, AvrMemoryType.Eeprom);
                offset += cnt;
            }
        }

        private void WriteFlash(int start, byte[] data, int dataStart, int dataLength) {
            var offset = start;
            var end = start + dataLength;
            while (offset < end) {
                _client.LoadAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);

                _client.ProgramPage(data, offset - start + dataStart, cnt, AvrMemoryType.Flash);
                offset += cnt;
            }
        }

        private void ReadFlash(int address, byte[] data, int dataStart, int dataLength) {
            var offset = address;
            var end = address + dataLength;
            while (offset < end) {
                _client.LoadAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);
                _client.ReadPage(data, offset - address + dataStart, cnt, AvrMemoryType.Flash);
                offset += cnt;
            }
        }

        public void Dispose() {
            _client.Dispose();
        }
    }
}
