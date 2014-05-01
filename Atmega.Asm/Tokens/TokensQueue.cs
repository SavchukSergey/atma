﻿using System;
using System.Collections.Generic;

namespace Atmega.Asm.Tokens {
    public class TokensQueue {

        private readonly Queue<Token> _queue;

        public TokensQueue(IEnumerable<Token> tokens) {
            _queue = new Queue<Token>(tokens);
        }

        public Token Read() {
            return _queue.Dequeue();
        }

        public Token Read(TokenType type) {
            var token = _queue.Dequeue();
            if (token.Type != type) throw new TokenException("Unexpected token", token);
            return token;
        }

        public Token Peek() {
            return _queue.Peek();
        }

        public int Count {
            get { return _queue.Count; }
        }
    }
}
