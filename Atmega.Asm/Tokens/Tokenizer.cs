using System;
using System.Collections.Generic;

namespace Atmega.Asm.Tokens {
    public class Tokenizer {

        private void SkipWhitespace(ref string content, ref int chIndex) {
            while (chIndex < content.Length) {
                var ch = content[chIndex];
                if (ch != ' ' && ch != '\t') return;
                chIndex++;
            }
        }

        private void SkipNewLineChar(ref string content, ref int pos, char firstChar) {
            SkipOne(ref content, ref pos, firstChar == '\n' ? '\r' : '\n');
        }

        private void SkipOne(ref string content, ref int chIndex, char match) {
            if (chIndex < content.Length) {
                if (content[chIndex] == match) {
                    chIndex++;
                }
            }
        }

        private void SkipLine(ref string content, ref int pos) {
            while (pos < content.Length) {
                var ch = content[pos++];
                if (ch == 0x0d) {
                    SkipOne(ref content, ref pos, (char)0x0a);
                    return;
                }
                if (ch == 0x0a) {
                    SkipOne(ref content, ref pos, (char)0x0d);
                    return;
                }
            }
        }

        private string ReadString(ref string content, ref int pos) {
            var quoteCh = content[pos++];
            var token = "";
            while (pos < content.Length) {
                var ch = content[pos++];
                if (ch == '\r' || ch == '\n') {
                    throw new Exception("Missing end quote");
                }
                if (ch == quoteCh) {
                    if (pos >= content.Length) return token;
                    var next = content[pos++];
                    if (next == quoteCh) {
                        token += ch;
                    } else {
                        pos--;
                        return token;
                    }
                } else {
                    token += ch;
                }
            }
            throw new Exception("Missing end quote");
        }

        private string ReadLiteral(ref string content, ref int chIndex) {
            var token = "";
            while (chIndex < content.Length) {
                var ch = content[chIndex++];
                if (IsPunctuation(ch)) {
                    chIndex--;
                    return token;
                }
                token += ch;
            }
            return token;
        }

        public IList<Token> Read(string content, string fileName = null) {
            var res = new List<Token>();
            var pos = 0;
            var lineNumber = 1;
            while (pos < content.Length) {
                SkipWhitespace(ref content, ref pos);
                if (pos >= content.Length) break;

                var ch = content[pos++];
                switch (ch) {
                    case '\r':
                    case '\n':
                        SkipNewLineChar(ref content, ref pos, ch);
                        lineNumber++;
                        res.Add(new Token {
                            Type = TokenType.NewLine,
                            Position = new TokenPosition { File = fileName, Line = lineNumber }
                        });
                        break;
                    case ';':
                        SkipLine(ref content, ref pos);
                        lineNumber++;
                        break;
                    case '"':
                    case '\'':
                        pos--;
                        res.Add(new Token {
                            Type = TokenType.String,
                            StringValue = ReadString(ref content, ref pos),
                            Position = new TokenPosition { File = fileName, Line = lineNumber }
                        });
                        break;
                    case '\\':
                        SkipWhitespace(ref content, ref pos);
                        if (pos >= content.Length) throw new Exception("Unexpected end of file");
                        ch = content[pos++];
                        if (ch == '\r' || ch == '\n') {
                            SkipNewLineChar(ref content, ref pos, ch);
                            lineNumber++;
                        } else {
                            throw new Exception("Unexpected character after backslash");
                        }
                        break;
                    default:
                        if (IsPunctuation(ch)) {
                            res.Add(new Token {
                                Type = TokenType.Punctuation,
                                StringValue = ch.ToString(),
                                Position = new TokenPosition { File = fileName, Line = lineNumber }
                            });
                        } else {
                            pos--;
                            res.Add(new Token {
                                Type = TokenType.Literal,
                                StringValue = ReadLiteral(ref content, ref pos),
                                Position = new TokenPosition { File = fileName, Line = lineNumber }
                            });
                        }
                        break;
                }
            }
            return res;
        }

        public bool IsPunctuation(char ch) {
            switch (ch) {
                case ' ':
                case '+':
                case '-':
                case '/':
                case '*':
                case '=':
                case '<':
                case '>':
                case '(':
                case ')':
                case '[':
                case ']':
                case '{':
                case '}':
                case ':':
                case ',':
                    return true;
                default:
                    return false;
            }
        }

        private static char[] SymbolCharacters = { '\t', (char)0x0A, (char)0x0D, (char)0x1A, '|', '&', '~', '#', '`', ';', '\\' };

    }
}
