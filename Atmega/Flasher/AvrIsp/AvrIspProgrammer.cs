using System;

namespace Atmega.Flasher.AvrIsp {
    public class AvrIspProgrammer : IProgrammer {

        private readonly AvrIspClient _client;
        private const int BLOCK_SIZE = 1024;

        public AvrIspProgrammer(AvrIspClient client) {
            _client = client;
        }

        public void Start() {
            _client.Open();
            _client.ResetDevice();
            _client.StartProgram();
        }

        public void Stop() {
            _client.EndProgram();
            _client.Close();
        }

        public byte[] ReadPage(int start, int length, AvrMemoryType memType) {
            switch (memType) {
                case AvrMemoryType.Eeprom:
                    return ReadEeprom(start, length);
                case AvrMemoryType.Flash:
                    return ReadFlash(start, length);
                default:
                    throw new NotSupportedException();
            }
        }

        public void WritePage(int start, AvrMemoryType memType, byte[] data) {
            switch (memType) {
                case AvrMemoryType.Eeprom:
                    WriteEeprom(start, data);
                    break;
                case AvrMemoryType.Flash:
                    WriteFlash(start, data);
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

        private void WriteEeprom(int start, byte[] data) {
            var offset = start;
            var end = start + data.Length;
            while (offset < end) {
                _client.SetAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);

                _client.WriteEeprom(data, offset, cnt);
                offset += cnt;
            }
        }

        private byte[] ReadEeprom(int start, int length) {
            var offset = start;
            var end = start + length;
            var result = new byte[length];
            while (offset < end) {
                _client.SetAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);

                var data = _client.ReadEeprom(cnt);

                foreach (var bt in data) {
                    result[offset - start] = bt;
                    offset++;
                }
            }

            return result;
        }

        private void WriteFlash(int start, byte[] data) {
            var offset = start;
            var end = start + data.Length;
            while (offset < end) {
                _client.SetAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);

                _client.WriteFlash(data, offset, cnt);
                offset += cnt;
            }
        }

        private byte[] ReadFlash(int start, int length) {
            var offset = start;
            var end = start + length;
            var result = new byte[length];
            while (offset < end) {
                _client.SetAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);

                var data = _client.ReadFlash(cnt);

                foreach (var bt in data) {
                    result[offset - start] = bt;
                    offset++;
                }
            }

            return result;
        }

        public void Dispose() {
            _client.Dispose();
        }
    }
}
