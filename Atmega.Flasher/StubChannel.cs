﻿using System;
using Atmega.Flasher.IO;

namespace Atmega.Flasher {
    public class StubChannel : IAvrChannel {

        public void Dispose() {
        }

        public void Open() {
        }

        public void Close() {
        }

        public void ToggleReset(bool val) {
        }

        public void SendByte(byte bt) {
        }

        public byte ReceiveByte() {
            return 0;
        }
    }
}