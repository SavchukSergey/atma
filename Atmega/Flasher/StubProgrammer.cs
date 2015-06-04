using System;
using System.Threading;

namespace Atmega.Flasher {
    public class StubProgrammer : IProgrammer {

        private readonly byte[] _flash = new byte[32768];
        private readonly byte[] _eeprom = new byte[1024];
        private AtmegaLockBits _lockBits;

        public void Dispose() {
        }

        public void Start() {
        }

        public void Stop() {
        }

        public void ReadPage(int address, AvrMemoryType memType, byte[] data, int dataStart, int dataLength) {
            var mem = memType == AvrMemoryType.Flash ? _flash : _eeprom;
            Thread.Sleep(50);
            for (var i = 0; i < dataLength; i++) {
                data[i + dataStart] = mem[(i + address) % mem.Length];
            }
        }

        public void WritePage(int address, AvrMemoryType memType, byte[] data, int dataStart, int dataLength) {
            Thread.Sleep(50);
            for (var i = 0; i < data.Length; i++) {
                switch (memType) {
                    case AvrMemoryType.Flash:
                        _flash[(i + address) % _flash.Length] = data[i + dataStart];
                        break;
                    case AvrMemoryType.Eeprom:
                        _eeprom[(i + address) % _eeprom.Length] = data[i + dataStart];
                        break;
                    default: throw new NotSupportedException();
                }
            }
        }

        public AtmegaLockBits ReadLockBits() {
            return _lockBits;
        }

        public void WriteLockBits(AtmegaLockBits bits) {
            _lockBits = bits;
        }

        public void EraseDevice() {
        }
    }
}
