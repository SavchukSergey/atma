using System.Collections.Generic;
using System.IO;
using Atmega.Asm.IO;
using Atmega.Asm.Opcodes;
using Atmega.Asm.Tokens;

namespace Atmega.Asm {
    public class Assembler {
        private readonly IAsmSource _source;

        public Assembler(IAsmSource source) {
            _source = source;
        }

        public AsmContext Load(string fileName) {
            var content = _source.LoadContent(fileName);
            return Assemble(content, fileName);
        }

        public AsmContext Assemble(string content, string fileName = null) {
            var symbols = new AsmSymbols();
            IList<Token> tokens = new List<Token>();
            LoadRecursive(content, tokens, fileName);

            tokens = ProcessSymbolConstants(tokens, symbols);

            AsmContext last = null;
            for (var i = 1; i < 10; i++) {
                var context = new AsmContext {
                    Queue = new TokensQueue(tokens),
                    Symbols = symbols,
                    Pass = i
                };
                var parser = new AsmParser(context);
                AssemblePass(context, parser);
                if (TheSame(last, context)) break;
                last = context;
            }
            return last;
        }

        public void SaveElf(AsmContext context, string fileName) {
            using (var file = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read)) {
                SaveElf(context, file);
                file.Flush();
                file.Close();
            }
        }

        public void SaveElf(AsmContext context, Stream stream) {
           new ElfBuilder().Save(context, stream);
        }


        protected IList<Token> ProcessSymbolConstants(IList<Token> tokens, AsmSymbols symbols) {
            var result = new List<Token>();

            for (var i = 0; i < tokens.Count; ) {
                var nameToken = tokens[i++];
                if (i < tokens.Count) {
                    var equToken = tokens[i];
                    if (equToken.Type == TokenType.Literal && equToken.StringValue.ToLower() == "equ") {
                        i++;
                        var meaning = new List<Token>();
                        CopyLine(tokens, meaning, symbols, ref i);
                        symbols.SymbolicConstants[nameToken.StringValue] = meaning;
                    } else {
                        i--;
                        CopyLine(tokens, result, symbols, ref i);
                    }
                } else {
                    i--;
                    CopyLine(tokens, result, symbols, ref i);
                }
            }
            return result;

        }

        protected IList<Token> LoadRecursive(string content, IList<Token> result, string fileName = null) {
            var tokenizer = new Tokenizer();
            var fileTokens = tokenizer.Read(new FileSource { FileName = fileName, Content = content });

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

                    var path = _source.ResolveFile(nameToken.StringValue, fileName);
                    if (string.IsNullOrWhiteSpace(path)) throw new TokenException("file is not found " + nameToken.StringValue, token);
                    var otherContent = _source.LoadContent(path);
                    LoadRecursive(otherContent, result, nameToken.StringValue);
                    result.Add(new Token { Type = TokenType.NewLine });
                } else {
                    i--;
                    CopyLine(fileTokens, result, ref i);
                }
            }
            return result;
        }

        private static void CopyLine(IList<Token> source, IList<Token> target, ref int pointer) {
            while (pointer < source.Count) {
                var tkn = source[pointer++];
                target.Add(tkn);
                if (tkn.Type == TokenType.NewLine) break;
            }
        }

        private static void CopyLine(IList<Token> source, IList<Token> target, AsmSymbols symbols, ref int pointer) {
            while (pointer < source.Count) {
                var tkn = source[pointer++];
                if (tkn.Type == TokenType.NewLine) {
                    target.Add(tkn);
                    break;
                }
                IList<Token> replaced;
                if (symbols.SymbolicConstants.TryGetValue(tkn.StringValue, out replaced)) {
                    foreach (var repl in replaced) {
                        if (repl.Type != TokenType.NewLine) {
                            var item = repl;
                            item.Position = tkn.Position;
                            target.Add(item);
                        }
                    }
                } else {
                    target.Add(tkn);
                }
            }
        }

        private static bool TheSame(AsmContext prev, AsmContext current) {
            if (prev == null) return false;
            return current.CodeSection.TheSame(prev.CodeSection);
        }

        private static void AssemblePass(AsmContext context, AsmParser parser) {
            while (context.Queue.Count > 0) {
                SkipEmptyLines(context);
                if (context.Queue.Count == 0) break;

                AssembleLine(context, parser);
            }
        }

        private static void AssembleLine(AsmContext context, AsmParser parser) {
            var token = context.Queue.Read(TokenType.Literal);
            try {
                if (CheckLabel(token, context)) {
                    return;
                }
                if (CheckData(token, context, parser)) {
                    return;
                }

                switch (token.StringValue.ToLower()) {
                    case "section":
                        ProcessSection(context);
                        break;
                    case "org":
                        ProcessOrg(parser, context.CurrentSection);
                        break;
                    default:
                        var opcode = AvrOpcodes.Get(token.StringValue);
                        if (opcode != null) {
                            opcode.Compile(parser, context.CurrentSection);
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
            } catch (PureSectionDataException exc) {
                throw new PureSectionDataException(exc.Message, token);
            }
        }

        private static bool CheckLabel(Token token, AsmContext context) {
            if (context.Queue.Count > 0) {
                var next = context.Queue.Peek();
                if (next.Type == TokenType.Colon) {
                    context.Queue.Read();
                    context.DefineLabel(token);
                    return true;
                }
            }
            return false;
        }

        private static bool CheckData(Token token, AsmContext context, AsmParser parser) {
            if (IsDataDirective(token)) {
                ProcessDataDirective(token, parser, context.CurrentSection);
                return true;
            }
            if (!context.Queue.IsEndOfLine) {
                var preview = context.Queue.Peek();
                if (IsDataDirective(preview)) {
                    context.Queue.Read(TokenType.Literal);
                    context.DefineLabel(token);
                    ProcessDataDirective(preview, parser, context.CurrentSection);
                    return true;
                }
            }
            return false;
        }

        private static void SkipEmptyLines(AsmContext context) {
            while (context.Queue.Count > 0) {
                Token token = context.Queue.Peek();
                if (token.Type != TokenType.NewLine) return;
                context.Queue.Read();
            }
        }

        private static void ProcessSection(AsmContext context) {
            var typeToken = context.Queue.Read();
            if (typeToken.Type != TokenType.Literal) {
                throw new TokenException("expected section type", typeToken);
            }

            var type = typeToken.ParseSectionType();
            if (type == AsmSectionType.None) {
                throw new TokenException("invalid section type", typeToken);
            }
            context.SetSection(type);
        }

        private static void ProcessOrg(AsmParser parser, AsmSection output) {
            var val = parser.CalculateExpression();
            output.Offset = (int)val;
        }

        private static void ProcessDataDirective(Token token, AsmParser parser, AsmSection output) {
            switch (token.StringValue.ToLower()) {
                case "db":
                    ProcessDataBytes(parser, output);
                    break;
                case "dw":
                    ProcessDataWords(parser, output);
                    break;
                case "rb":
                    ProcessReserveBytes(parser, output);
                    break;
                case "rw":
                    ProcessReserveWords(parser, output);
                    break;
                default:
                    throw new TokenException("invalid directive " + token.StringValue, token);
            }
        }

        private static void ProcessDataBytes(AsmParser parser, AsmSection output) {
            do {
                if (parser.IsEndOfLine) {
                    throw new TokenException("expected data bytes", parser.LastReadToken);
                }

                var token = parser.PeekToken();

                if (token.Type == TokenType.String) {
                    token = parser.ReadToken(TokenType.String);
                    foreach (var ch in token.StringValue) {
                        if (ch > 255) {
                            throw new TokenException("unicode character cannot be translated to byte", token);
                        }
                        output.EmitByte((byte)ch);
                    }
                } else {
                    var val = parser.ReadByte();
                    output.EmitByte(val);
                }

                if (parser.IsEndOfLine) break;

                var commaPreview = parser.PeekToken();
                if (commaPreview.Type != TokenType.Comma) break;
                parser.ReadToken(TokenType.Comma);
            } while (true);
        }

        private static void ProcessDataWords(AsmParser parser, AsmSection output) {
            do {
                if (parser.IsEndOfLine) {
                    throw new TokenException("expected data words", parser.LastReadToken);
                }

                var token = parser.PeekToken();

                if (token.Type == TokenType.String) {
                    token = parser.ReadToken(TokenType.String);
                    foreach (var ch in token.StringValue) {
                        output.EmitWord(ch);
                    }
                } else {
                    var val = parser.ReadUshort();
                    output.EmitWord(val);
                }

                if (parser.IsEndOfLine) break;

                var commaPreview = parser.PeekToken();
                if (commaPreview.Type != TokenType.Comma) break;
                parser.ReadToken(TokenType.Comma);
            } while (true);
        }

        private static void ProcessReserveBytes(AsmParser parser, AsmSection output) {
            var cnt = parser.CalculateExpression();
            output.ReserveBytes((int)cnt);
        }

        private static void ProcessReserveWords(AsmParser parser, AsmSection output) {
            var cnt = parser.CalculateExpression();
            output.ReserveBytes((int)cnt * 2);
        }

        private static bool IsDataDirective(Token token) {
            if (token.Type != TokenType.Literal) return false;
            switch (token.StringValue.ToLower()) {
                case "db": return true;
                case "dw": return true;
                case "rb": return true;
                case "rw": return true;
                default: return false;
            }
        }
    }
}
