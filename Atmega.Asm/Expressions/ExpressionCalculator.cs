using System;
using System.Collections;
using System.Collections.Generic;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Expressions {
    public class ExpressionCalculator {

        public long Calculate(AsmContext context) {
            if (context.Queue.Count == 0) {
                throw new Exception("unexpected end of file"); //todo custom exception
            }
            var stack = new Stack<BaseExpression>();
            do {
                var preview = context.Queue.Peek();
                if (preview.Type == TokenType.NewLine) break;
                if (preview.Type == TokenType.Comma) break;

                var token = context.Queue.Read();
                if (token.Type == TokenType.Integer) {
                    stack.Push(new NumberExpression { Value = token.IntegerValue });
                } else if (token.Type == TokenType.Literal) {
                    if (token.StringValue == "$") {
                        stack.Push(new NumberExpression { Value = context.Offset });
                    } else {
                        var lblValue = context.GetLabel(token.StringValue);
                        if (lblValue == null) {
                            throw new TokenException("unknown symbol " + token.StringValue, token);
                        }
                        stack.Push(new NumberExpression { Value = (long)lblValue });
                    }
                }

            } while (context.Queue.Count > 0);

            if (stack.Count != 1) {
                throw new Exception("unexpected end of line"); //todo custom exception
            }

            return stack.Pop().Value;
        }
    }
}
