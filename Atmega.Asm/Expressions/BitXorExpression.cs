﻿namespace Atmega.Asm.Expressions {
    public class BitXorExpression : BinaryExpression {

        public BitXorExpression(BaseExpression left, BaseExpression right)
            : base(left, right) {
        }

        public override long Evaluate() {
            return Left.Evaluate() ^ Right.Evaluate();
        }
    }
}
