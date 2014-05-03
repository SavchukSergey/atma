namespace Atmega.Asm.Expressions {
    public class DivExpression : BinaryExpression {

        public DivExpression(BaseExpression left, BaseExpression right)
            : base(left, right) {
        }

        public override long Evaluate() {
            return Left.Evaluate() / Right.Evaluate();
        }
    }
}
