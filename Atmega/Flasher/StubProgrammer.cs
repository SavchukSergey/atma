using System;
using System.Linq;
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
            Thread.Sleep(50);
            switch (memType) {
                case AvrMemoryType.Flash:
                    return _flash.Skip(start).Take(length).ToArray();
                case AvrMemoryType.Eeprom:
                    return _eeprom.Skip(start).Take(length).ToArray();
                default: throw new NotSupportedException();
            }
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
