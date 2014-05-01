using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm {
    public class TokenException : Exception {
        private readonly Token _token;

        public TokenException(string message, Token token)
            : base(message) {
            _token = token;
        }

        public Token Token {
            get { return _token; }
        }
    }
}
