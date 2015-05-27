using System;

namespace Atmega.Flasher {
    public interface IProgrammer : IDisposable {

        void Start();

        void Stop();

        byte[] ReadPage(int start, int length, AvrMemoryType memType, Action<ProgressCallbackData> progressCallbackData = null);

        void WritePage(int start, AvrMemoryType memType, byte[] data, Action<ProgressCallbackData> progressCallbackData = null);

    }
}
