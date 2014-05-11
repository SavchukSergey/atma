using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm {
    public class PureSectionDataException : TokenException {
        
        public PureSectionDataException(string message, Token token)
            : base(message, token) {
        }

        public PureSectionDataException(string message)
            : base(message, new Token()) {
        }

    }
}
