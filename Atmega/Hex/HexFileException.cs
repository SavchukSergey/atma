using System;

namespace Atmega.Hex {
    public class HexFileException : Exception {
        public HexFileException(string message)
            : base(message) {
        }
    }
}
