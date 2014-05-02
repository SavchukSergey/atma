namespace Atmega.Asm.Expressions {
    public class MulExpression : BinaryExpression {

        public MulExpression(BaseExpression left, BaseExpression right)
            : base(left, right) {
        }

        public override long Evaluate() {
            return Left.Evaluate() * Right.Evaluate();
        }
    }
}
