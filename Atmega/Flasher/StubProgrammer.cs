using System.Threading;

namespace Atmega.Flasher {
    public class StubProgrammer : IProgrammer {
        public void Dispose() {
        }

        public void Start() {
        }

        public void Stop() {
        }

        public byte[] ReadPage(int start, int length, AvrMemoryType memType) {
            Thread.Sleep(50);
            return new byte[length];
        }

        public void WritePage(int start, AvrMemoryType memType, byte[] data) {
            Thread.Sleep(550);
        }
    }
}
