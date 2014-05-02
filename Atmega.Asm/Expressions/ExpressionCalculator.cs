using System;
using System.Collections.Generic;
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

        private BaseExpression ParseWithPriority(TokensQueue tokens, int priority) {
            if (tokens.Count == 0) {
                throw new Exception("unexpected end of file"); //todo custom exception
            }
            var stack = new Stack<BaseExpression>();
            do {
                var preview = tokens.Peek();
                if (preview.Type == TokenType.NewLine) break;
                if (preview.Type == TokenType.Comma) break;
                if (preview.Type == TokenType.CloseParenthesis) break;
                var tokenPriority = GetPriority(preview.Type);
                if (tokenPriority >= 0 && tokenPriority < priority) {
                    //TODO: range check and count
                    return stack.Pop();
                }

                var token = tokens.Read();
                switch (token.Type) {
                    case TokenType.Integer:
                        stack.Push(new NumberExpression { Value = token.IntegerValue });
                        break;
                    case TokenType.Plus:
                        //TODO: range check and count
                        var otherAdd = ParseWithPriority(tokens, tokenPriority + 1);
                        stack.Push(new AddExpression(stack.Pop(), otherAdd));
                        break;
                    case TokenType.Multiply:
                        //TODO: range check and count
                        var otherMul = ParseWithPriority(tokens, tokenPriority + 1);
                        stack.Push(new MulExpression(stack.Pop(), otherMul));
                        break;
                    case TokenType.Literal:
                        stack.Push(ParseLiteral(token));
                        break;
                    case TokenType.OpenParenthesis: {
                            var inner = Parse(tokens);
                            stack.Push(inner);
                            if (tokens.Count == 0) {
                                throw new Exception("missing closing parenthesis");
                            }
                            var close = tokens.Read();
                            if (close.Type != TokenType.CloseParenthesis) {
                                throw new TokenException("expected closing parenthesis", close);
                            }
                        }
                        break;
                    default:
                        throw new TokenException("unexpected token", token);
                }

            } while (tokens.Count > 0);

            if (stack.Count != 1) {
                throw new Exception("unexpected end of line"); //todo custom exception
            }

            return stack.Pop();
        }

        private static int GetPriority(TokenType type) {
            switch (type) {
                case TokenType.Plus:
                case TokenType.Minus:
                    return 0;
                case TokenType.Multiply:
                case TokenType.Divide:
                    return 1;
                default:
                    return -1;
            }
        }
    }
}
