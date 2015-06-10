using System;

namespace Atmega.Flasher.IO {
    public interface IAvrChannel : IDisposable {

        void Open();

        void Close();

        void ToggleReset(bool val);

        void SendByte(byte bt);

        byte ReceiveByte();

    }
}
