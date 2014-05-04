using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Expressions {
    public class ExpressionCalculator {

        private readonly AsmContext _context;

        public ExpressionCalculator(AsmContext context) {
            _context = context;
        }

        protected BaseExpression ParseLiteral(Token token) {
            if (token.StringValue == "$") {
                return new NumberExpression { Value = _context.Offset };
            }
            var lblValue = _context.GetLabel(token.StringValue);
            if (lblValue == null) {
                throw new TokenException("unknown symbol " + token.StringValue, token);
            }
            return new NumberExpression { Value = (long)lblValue };
        }

        public BaseExpression Parse(string source) {
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Read(source);
            var queue = new TokensQueue(tokens);
            return Parse(queue);
        }

        public BaseExpression Parse(TokensQueue tokens) {
            return ParseWithPriority(tokens, 0);
        }

        private BaseExpression ParseOperand(TokensQueue tokens) {
            if (tokens.Count == 0) {
                throw new Exception("unexpected end of expression");
            }

            var token = tokens.Read();
            switch (token.Type) {
                case TokenType.Integer: return new NumberExpression { Value = token.IntegerValue };
                case TokenType.Literal: return ParseLiteral(token);
                case TokenType.NewLine: throw new TokenException("value expected", token);
                case TokenType.Minus: return new NegateExpression(ParseOperand(tokens));
                case TokenType.OpenParenthesis:
                    var inner = Parse(tokens);
                    if (tokens.Count == 0) {
                        throw new Exception("missing closing parenthesis");
                    }
                    var close = tokens.Read();
                    if (close.Type != TokenType.CloseParenthesis) {
                        throw new TokenException("expected closing parenthesis", close);
                    }
                    return inner;
                default:
                    throw new TokenException("unexpected token " + token.StringValue, token);
            }
        }

        private BaseExpression ParseWithPriority(TokensQueue tokens, int priority) {

            BaseExpression left = ParseOperand(tokens);
            while (tokens.Count > 0) {
                var preview = tokens.Peek();
                switch (preview.Type) {
                    case TokenType.NewLine:
                    case TokenType.Comma:
                    case TokenType.CloseParenthesis:
                        return left;
                }

                var tokenPriority = GetPriority(preview.Type);
                if (tokenPriority >= 0 && tokenPriority < priority) {
                    if (left != null) return left;
                    throw new Exception("some case");
                }

                var token = tokens.Read();
                switch (token.Type) {
                    case TokenType.Plus:
                    case TokenType.Minus:
                    case TokenType.Multiply:
                    case TokenType.Divide:
                    case TokenType.Mod:
                    case TokenType.LeftShift:
                    case TokenType.BitOr:
                    case TokenType.BitAnd:
                        left = ProcessBinaryExpression(token, left, tokens);
                        break;
                    default:
                        throw new TokenException("unexpected token " + token.StringValue, token);
                }
            }

            return left;
        }

        private BaseExpression ProcessBinaryExpression(Token opToken, BaseExpression left, TokensQueue tokens) {
            var tokenPriority = GetPriority(opToken.Type);
            var other = ParseWithPriority(tokens, tokenPriority + 1);
            switch (opToken.Type) {
                case TokenType.Plus: return new AddExpression(left, other);
                case TokenType.Minus: return new SubExpression(left, other);
                case TokenType.Multiply:return new MulExpression(left, other);
                case TokenType.Divide: return new DivExpression(left, other);
                case TokenType.Mod: return new ModExpression(left, other);
                case TokenType.LeftShift: return new ShiftLeftExpression(left, other);
                case TokenType.BitOr: return new BitOrExpression(left, other);
                case TokenType.BitAnd: return new BitAndExpression(left, other);
                default:
                    throw new TokenException("unexpected operator", opToken);
            }
        }

        private static int GetPriority(TokenType type) {
            switch (type) {
                case TokenType.Plus:
                case TokenType.Minus:
                    return 0;
                case TokenType.Multiply:
                case TokenType.Divide:
                    return 1;
                case TokenType.Mod:
                    return 2;

                case TokenType.BitOr:
                    return 3;

                case TokenType.LeftShift:
                case TokenType.RightShift:
                    return 4;

                default:
                    return -1;
            }
        }
    }
}
