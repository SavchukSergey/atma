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

        public void ReadPage(int address, AvrMemoryType memType, byte[] data, int dataStart, int dataLength) {
            for (var i = 0; i < dataLength; i++) {
                switch (memType) {
                    case AvrMemoryType.Flash:
                        data[i + dataStart] = _client.ReadFlashByte((ushort)(address + i));
                        break;
                    case AvrMemoryType.Eeprom:
                        data[i + dataStart] = _client.ReadEepromMemory((ushort)(address + i));
                        break;
                }
            }
        }

        public void WritePage(int address, AvrMemoryType memType, byte[] data, int dataStart, int dataLength) {
            for (var i = 0; i < dataLength; i++) {
                switch (memType) {
                    case AvrMemoryType.Flash:
                        _client.LoadProgramMemoryPageByte((ushort)(address + i), data[i + dataStart]);
                        break;
                    case AvrMemoryType.Eeprom:
                        _client.WriteEepromMemory((ushort)(address + i), data[i + dataStart]);
                        break;
                }
            }
            if (memType == AvrMemoryType.Flash) {
                _client.WriteProgramMemoryPage((ushort) address);
            }
        }

        public AtmegaLockBits ReadLockBits() {
            return new AtmegaLockBits { Value = _client.ReadLockBits() };
        }

        public void WriteLockBits(AtmegaLockBits bits) {
            _client.WriteLockBits(bits.Value);
        }

        public void EraseDevice() {
            _client.ChipErase();
        }
    }
}
