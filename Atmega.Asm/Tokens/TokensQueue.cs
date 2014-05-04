using System.Collections.Generic;

namespace Atmega.Asm.Tokens {
    public class TokensQueue {

        private readonly Queue<Token> _queue;
        private Token _lastToken;

        public TokensQueue(IEnumerable<Token> tokens) {
            _queue = new Queue<Token>(tokens);
        }

        public Token Read() {
            _lastToken = _queue.Dequeue();
            return _lastToken;
        }

        public Token Read(TokenType type) {
            var token = Read();
            if (token.Type != type) throw new TokenException("Unexpected token", token);
            return token;
        }

        public Token Peek() {
            return _queue.Peek();
        }

        public int Count {
            get { return _queue.Count; }
        }

        public Token LastReadToken {
            get { return _lastToken; }
        }

        public bool IsEndOfLine {
            get { return _queue.Count == 0 || _queue.Peek().Type == TokenType.NewLine; }
        }
    }
}
