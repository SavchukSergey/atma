using System;
using System.Threading;

namespace Atmega.Flasher {
    public class StubProgrammer : IProgrammer {

        private readonly byte[] _flash = new byte[32768];
        private readonly byte[] _eeprom = new byte[1024];

        public void Dispose() {
        }

        public void Start() {
        }

        public void Stop() {
        }

        public byte[] ReadPage(int start, int length, AvrMemoryType memType) {
            var res = new byte[length];
            var mem = memType == AvrMemoryType.Flash ? _flash : _eeprom;
            Thread.Sleep(50);
            for (var i = 0; i < length; i++) {
                res[i] = mem[i % mem.Length];
            }
            return res;
        }

        public void WritePage(int start, AvrMemoryType memType, byte[] data) {
            Thread.Sleep(50);
            for (var i = 0; i < data.Length; i++) {
                switch (memType) {
                    case AvrMemoryType.Flash:
                        _flash[i + start] = data[i];
                        break;
                    case AvrMemoryType.Eeprom:
                        _eeprom[i + start] = data[i];
                        break;
                    default: throw new NotSupportedException();
                }
            }
        }
    }
}
