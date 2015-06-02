using System;

namespace Atmega.Flasher {
    public interface IProgrammer : IDisposable {

        void Start();

        void Stop();

        byte[] ReadPage(int start, int length, AvrMemoryType memType);

        void WritePage(int start, AvrMemoryType memType, byte[] data);

        AtmegaLockBits ReadLockBits();

        void WriteLockBits(AtmegaLockBits bits);

        void EraseDevice();

    }
}
