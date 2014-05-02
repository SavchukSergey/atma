using System;
using System.Collections.Generic;
using System.IO;
using Atmega.Asm.Opcodes;
using Atmega.Asm.Tokens;

namespace Atmega.Asm {
    public class Assembler {


        public AsmContext Assemble(string content) {
            var tokens = new List<Token>();
            LoadRecursive(content, tokens);

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

        protected IList<Token> LoadRecursive(string content, IList<Token> result) {
            var tokenizer = new Tokenizer();
            var fileTokens = tokenizer.Read(content);

            for (var i = 0; i < fileTokens.Count; ) {
                var token = fileTokens[i++];
                if (token.Type == TokenType.Literal && token.StringValue == "include") {
                    if (i >= fileTokens.Count) {
                        throw new TokenException("file name expected", token);
                    }
                    var nameToken = fileTokens[i++];
                    if (nameToken.Type != TokenType.String) {
                        throw new TokenException("file name expected", token);
                    }

                    if (i < fileTokens.Count) {
                        var nlToken = fileTokens[i++];
                        if (nlToken.Type != TokenType.NewLine) {
                            throw new TokenException("extra characters on line", token);
                        }
                    }

                    var otherContent = LoadContent(nameToken.StringValue);
                    LoadRecursive(otherContent, result);
                } else {
                    result.Add(token);
                    while (i < fileTokens.Count) {
                        var tkn = fileTokens[i++];
                        result.Add(tkn);
                        if (tkn.Type == TokenType.NewLine) break;
                    }
                }
            }
            return result;
        }

        protected virtual string LoadContent(string fileName) {
            using (var reader = new StreamReader(fileName)) {
                return reader.ReadToEnd();
            }
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
