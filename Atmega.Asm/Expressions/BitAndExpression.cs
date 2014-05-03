﻿namespace Atmega.Asm.Expressions {
    public class BitAndExpression : BinaryExpression {

        public BitAndExpression(BaseExpression left, BaseExpression right)
            : base(left, right) {
        }

        public override long Evaluate() {
            return Left.Evaluate() & Right.Evaluate();
        }
    }
}
