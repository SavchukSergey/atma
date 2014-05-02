using System;
using System.Collections.Generic;
using System.IO;
using Atmega.Asm.Opcodes;
using Atmega.Asm.Tokens;

namespace Atmega.Asm {
    public class Assembler {


        public AsmContext Load(string fileName) {
            var content = LoadContent(fileName);
            return Assemble(content, fileName);
        }

        public AsmContext Assemble(string content, string fileName = null) {
            IList<Token> tokens = new List<Token>();
            LoadRecursive(content, tokens, fileName);

            tokens = ProcessSymbolConstants(tokens);

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

        protected IList<Token> ProcessSymbolConstants(IList<Token> tokens) {
            var result = new List<Token>();

            IDictionary<string, IList<Token>> map = new Dictionary<string, IList<Token>>();

            for (var i = 0; i < tokens.Count; ) {
                var nameToken = tokens[i++];
                if (i < tokens.Count) {
                    var equToken = tokens[i];
                    if (equToken.Type == TokenType.Literal && equToken.StringValue.ToLower() == "equ") {
                        i++;
                        var meaning = new List<Token>();
                        CopyLine(tokens, meaning, map, ref i);
                        map[nameToken.StringValue] = meaning;
                    } else {
                        i--;
                        CopyLine(tokens, result, map, ref i);
                    }
                } else {
                    i--;
                    CopyLine(tokens, result, map, ref i);
                }
            }
            return result;

        }

        protected IList<Token> LoadRecursive(string content, IList<Token> result, string fileName = null) {
            var tokenizer = new Tokenizer();
            var fileTokens = tokenizer.Read(content, fileName);

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
                    i--;
                    CopyLine(fileTokens, result, ref i);
                }
            }
            return result;
        }

        private void CopyLine(IList<Token> source, IList<Token> target, ref int pointer) {
            while (pointer < source.Count) {
                var tkn = source[pointer++];
                target.Add(tkn);
                if (tkn.Type == TokenType.NewLine) break;
            }
        }

        private void CopyLine(IList<Token> source, IList<Token> target, IDictionary<string, IList<Token>> symbolConstants, ref int pointer) {
            while (pointer < source.Count) {
                var tkn = source[pointer++];
                if (tkn.Type == TokenType.NewLine) {
                    target.Add(tkn);
                    break;
                }
                IList<Token> replaced;
                if (symbolConstants.TryGetValue(tkn.StringValue, out replaced)) {
                    foreach (var repl in replaced) {
                        if (repl.Type != TokenType.NewLine) {
                            target.Add(repl);
                        }
                    }
                } else {
                    target.Add(tkn);
                }
            }
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
                if (CheckLabel(token, context)) {
                    continue;
                }

                switch (token.StringValue.ToLower()) {
                    case "section":
                        ProcessSection(context);
                        break;
                    case "org":
                        ProcessOrg(context);
                        break;
                    default:
                        var opcode = AvrOpcodes.Get(token.StringValue);
                        if (opcode != null) {
                            opcode.Compile(context);
                        } else {
                            throw new TokenException("Illegal instruction " + token.StringValue, token);
                        }
                        break;
                }
                if (context.Queue.Count > 0) {
                    var nl = context.Queue.Peek();
                    if (nl.Type != TokenType.NewLine) {
                        throw new TokenException("Extra characters on line", nl);
                    }
                }
            }
        }

        private bool CheckLabel(Token token, AsmContext context) {
            if (context.Queue.Count > 0) {
                var next = context.Queue.Peek();
                if (next.Type == TokenType.Colon) {
                    context.Queue.Read();
                    context.DefineLabel(token.StringValue);
                    return true;
                }
            }
            return false;
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

        private void ProcessOrg(AsmContext context) {
            if (context.Queue.Count == 0) {
                throw new Exception("org value expected");
            }
            var token = context.Queue.Read();
            if (token.Type != TokenType.Integer) {
                throw new Exception("integer value expected");
            }

            context.CodeOffset = (int)token.IntegerValue;
        }

    }
}
