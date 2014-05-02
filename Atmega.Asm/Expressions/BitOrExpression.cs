namespace Atmega.Asm.Expressions {
    public class BitOrExpression : BinaryExpression {
        public BitOrExpression(BaseExpression left, BaseExpression right)
            : base(left, right) {
        }

        public override long Evaluate() {
            return Left.Evaluate() | Right.Evaluate();
        }
    }
}
