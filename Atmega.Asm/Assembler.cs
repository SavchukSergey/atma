using System;
using System.Collections;
using System.Collections.Generic;
using Atmega.Asm.Opcodes;
using Atmega.Asm.Tokens;

namespace Atmega.Asm {
    public class Assembler {

        public AsmContext Assemble(string content) {
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Read(content);

            AsmContext last = null;
            for (var i = 0; i < 10; i++) {
                var context = new AsmContext {
                    Queue = new TokensQueue(tokens)
                };
                AssemblePass(context);
                if (TheSame(last, context)) break;
                last = context;
            }
            return last;
        }

        private bool TheSame(AsmContext prev, AsmContext current) {
            if (prev == null) return false;
            if (prev.Code.Count != current.Code.Count) return false;
            for (var i = 0; i < current.Code.Count; i++) {
                if (prev.Code[i] != current.Code[i]) return false;
            }
            return true;
        }

        private void AssemblePass(AsmContext context) {
            while (context.Queue.Count > 0) {
                SkipEmptyLines(context);
                if (context.Queue.Count == 0) break;

                var token = context.Queue.Read(TokenType.Literal);
                switch (token.StringValue.ToLower()) {
                    case "section":
                        ProcessSection(context);
                        break;
                    default:
                        var opcode = AvrOpcodes.Get(token.StringValue);
                        if (opcode != null) {
                            opcode.Compile(context);
                        } else {
                            throw new Exception("Illegal instruction " + token.StringValue);
                        }
                        break;
                }
                if (context.Queue.Count > 0) {
                    var nl = context.Queue.Peek();
                    if (nl.Type != TokenType.NewLine) {
                        throw new Exception("Extra characters on line");
                    }
                }
            }
        }

        private void SkipEmptyLines(AsmContext context) {
            while (context.Queue.Count > 0) {
                Token token = context.Queue.Peek();
                if (token.Type != TokenType.NewLine) return;
                context.Queue.Read();
            }
        }

        private void ProcessSection(AsmContext context) {
            var type = context.Queue.Read(TokenType.Literal);
        }
    }
}
