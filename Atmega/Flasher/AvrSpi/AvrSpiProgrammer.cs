using System;

namespace Atmega.Flasher.AvrSpi {
    public class AvrSpiProgrammer : IProgrammer {
        private readonly AvrSpiClient _client;

        public AvrSpiProgrammer(AvrSpiClient client) {
            _client = client;
        }

        public void Dispose() {
            _client.Dispose();
        }

        public void Start() {
            _client.Open();
            _client.StartProgram();
        }

        public void Stop() {
            _client.EndProgram();
            _client.Close();
        }

        public byte[] ReadPage(int start, int length, AvrMemoryType memType) {
            var res = new byte[length];
            for (var i = 0; i < length; i++) {
                switch (memType) {
                    case AvrMemoryType.Flash:
                        res[i] = _client.ReadFlashByte((ushort)(start + i));
                        break;
                    case AvrMemoryType.Eeprom:
                        res[i] = _client.ReadEepromMemory((ushort)(start + i));
                        break;
                }
            }
            return res;
        }

        public void WritePage(int start, AvrMemoryType memType, byte[] data) {
            for (var i = 0; i < data.Length; i++) {
                switch (memType) {
                    case AvrMemoryType.Flash:
                        _client.WriteFlashByte((ushort)(start + i), data[i]);
                        break;
                    case AvrMemoryType.Eeprom:
                        _client.WriteEepromMemory((ushort)(start + i), data[i]);
                        break;
                }
            }
        }

        public AtmegaLockBits ReadLockBits() {
            return new AtmegaLockBits { Value = _client.ReadLockBits() };
        }

        public void WriteLockBits(AtmegaLockBits bits) {
            _client.WriteLockBits(bits.Value);
        }
    }
}
