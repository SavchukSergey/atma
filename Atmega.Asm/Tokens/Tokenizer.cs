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

        private string ReadString(ref string content, ref int pos, TokenPosition position) {
            var quoteCh = content[pos++];
            var token = "";
            while (pos < content.Length) {
                var ch = content[pos++];
                if (ch == '\r' || ch == '\n') {
                    throw new TokenException("missing end quote", new Token { Position = position });
                }
                if (ch == '\\') {
                    if (pos >= content.Length) throw new TokenException("missing end quote", new Token { Position = position });
                    ch = content[pos++];
                    switch (ch) {
                        case 'n':
                            token += '\n';
                            break;
                        case 'r':
                            token += '\r';
                            break;
                        case 'a':
                            token += '\a';
                            break;
                        case 'b':
                            token += '\b';
                            break;
                        case 'f':
                            token += '\f';
                            break;
                        case 't':
                            token += '\t';
                            break;
                        case 'v':
                            token += '\v';
                            break;
                        case '\\':
                            token += '\\';
                            break;
                        case '0':
                            token += '\0';
                            break;
                        default:
                            throw new TokenException("invalid escape character " + ch, new Token { Position = position });
                    }
                } else if (ch == quoteCh) {
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
            throw new TokenException("missing end quote", new Token { Position = position });
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

        public IList<Token> Read(string content) {
            return Read(new FileSource { Content = content, FileName = null });
        }

        public IList<Token> Read(FileSource source) {
            var content = source.Content;
            var res = new List<Token>();
            var pos = 0;
            var lineNumber = 1;
            var lineStart = 0;
            while (pos < content.Length) {
                SkipWhitespace(ref content, ref pos);
                if (pos >= content.Length) break;
                var position = new TokenPosition { File = source, Line = lineNumber, LineStart = lineStart };

                var ch = content[pos++];
                var preview = (char)(0xffff);
                if (pos < content.Length) {
                    preview = content[pos];
                }
                if (ch == '>' && preview == '>') {
                    res.Add(new Token {
                        Type = TokenType.RightShift,
                        StringValue = ">",
                        Position = position
                    });
                    pos++;
                    continue;
                }
                if (ch == '<' && preview == '<') {
                    res.Add(new Token {
                        Type = TokenType.LeftShift,
                        StringValue = "<",
                        Position = position
                    });
                    pos++;
                    continue;
                }
                switch (ch) {
                    case '\r':
                    case '\n':
                        SkipNewLineChar(ref content, ref pos, ch);
                        lineNumber++;
                        lineStart = pos;
                        res.Add(new Token {
                            Type = TokenType.NewLine,
                            Position = position
                        });
                        break;
                    case ';':
                        SkipLine(ref content, ref pos);
                        lineNumber++;
                        lineStart = pos;
                        res.Add(new Token {
                            Type = TokenType.NewLine,
                            Position = position
                        });
                        break;
                    case '"':
                    case '\'':
                        pos--;
                        res.Add(new Token {
                            Type = TokenType.String,
                            StringValue = ReadString(ref content, ref pos, position),
                            Position = position
                        });
                        break;
                    case '\\':
                        SkipWhitespace(ref content, ref pos);
                        if (pos >= content.Length) throw new Exception("Unexpected end of file");
                        ch = content[pos++];
                        if (ch == '\r' || ch == '\n') {
                            SkipNewLineChar(ref content, ref pos, ch);
                            lineNumber++;
                            lineStart = pos;
                        } else {
                            throw new Exception("Unexpected character after backslash");
                        }
                        break;
                    default:
                        if (IsPunctuation(ch)) {
                            res.Add(new Token {
                                Type = GetPunctuationTokenType(ch),
                                StringValue = ch.ToString(),
                                Position = position
                            });
                        } else if (char.IsDigit(ch)) {
                            pos--;
                            res.Add(ReadIntegerToken(ref content, ref pos, position));
                        } else {
                            pos--;
                            res.Add(new Token {
                                Type = TokenType.Literal,
                                StringValue = ReadLiteral(ref content, ref pos),
                                Position = position
                            });
                        }
                        break;
                }
            }
            return res;
        }

        private Token ReadIntegerToken(ref string content, ref int pos, TokenPosition position) {
            var literal = ReadLiteral(ref content, ref pos).ToLower();
            if (literal.StartsWith("0x")) {
                return ParsePrefixedHexInteger(literal, position);
            }
            if (literal.EndsWith("h")) {
                return ParsePostfixedHexInteger(literal, position);
            }
            return ReadDecimalInteger(literal, position);
        }

        private Token ReadDecimalInteger(string literal, TokenPosition position) {
            long val = 0;
            var pos = 0;
            while (pos < literal.Length) {
                var ch = literal[pos++];
                if (char.IsDigit(ch)) {
                    val = val * 10 + (ch - '0');
                } else {
                    throw new TokenException("unexpected decimal symbol '" + ch + "'", new Token { Position = position, StringValue = literal });
                }
            }
            return new Token {
                Type = TokenType.Integer,
                IntegerValue = val,
                StringValue = literal,
                Position = position
            };
        }

        private Token ParsePrefixedHexInteger(string literal, TokenPosition position) {
            long val = 0;
            var pos = 2;
            if (pos >= literal.Length) throw new TokenException("Unexpeced end of hex constant", new Token { Position = position, StringValue = literal });
            while (pos < literal.Length) {
                var ch = literal[pos++];
                var hex = GetHexValue(ch);
                if (hex < 0) throw new TokenException("unexpected hex symbol '" + ch + "'", new Token { Position = position, StringValue = literal });
                val = val * 16 + hex;
            }
            return new Token {
                Type = TokenType.Integer,
                IntegerValue = val,
                StringValue = literal,
                Position = position
            };
        }

        private Token ParsePostfixedHexInteger(string literal, TokenPosition position) {
            long val = 0;
            var pos = 0;
            if (pos >= literal.Length) throw new TokenException("Unexpeced end of hex constant", new Token { Position = position, StringValue = literal });
            while (pos < literal.Length - 1) {
                var ch = literal[pos++];
                var hex = GetHexValue(ch);
                if (hex < 0) throw new TokenException("unexpected hex symbol '" + ch + "'", new Token { Position = position, StringValue = literal });
                val = val * 16 + hex;
            }
            return new Token {
                Type = TokenType.Integer,
                IntegerValue = val,
                StringValue = literal,
                Position = position
            };
        }

        private int GetHexValue(char ch) {
            if (ch >= '0' && ch <= '9') return ch - '0';
            if (ch >= 'A' && ch <= 'F') return ch - 'A' + 10;
            if (ch >= 'a' && ch <= 'f') return ch - 'a' + 10;
            return -1;
        }

        private bool IsPunctuation(char ch) {
            switch (ch) {
                case ' ':
                case '\t':
                case '\r':
                case '\n':
                case '+':
                case '-':
                case '/':
                case '*':
                case '%':
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
                case '|':
                case '&':
                case '^':
                case '~':
                case '#':
                case '`':
                case ';':
                case '\\':
                    return true;
                default:
                    return false;
            }
        }

        private static TokenType GetPunctuationTokenType(char ch) {
            switch (ch) {
                case ',': return TokenType.Comma;
                case ':': return TokenType.Colon;
                case '(': return TokenType.OpenParenthesis;
                case ')': return TokenType.CloseParenthesis;
                case '+': return TokenType.Plus;
                case '-': return TokenType.Minus;
                case '*': return TokenType.Multiply;
                case '/': return TokenType.Divide;
                case '%': return TokenType.Mod;
                case '<': return TokenType.Less;
                case '>': return TokenType.Greater;
                case '|': return TokenType.BitOr;
                case '&': return TokenType.BitAnd;
                case '^': return TokenType.BitXor;
                case ' ':
                case '\t':
                case '\r':
                case '\n':
                case '=':
                case '[':
                case ']':
                case '{':
                case '}':
                case '~':
                case '#':
                case '`':
                case ';':
                case '\\':
                    return TokenType.Punctuation;
                default:
                    return TokenType.None;
            }
        }

    }
}
