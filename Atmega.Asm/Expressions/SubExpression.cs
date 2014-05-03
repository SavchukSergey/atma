namespace Atmega.Asm.Expressions {
    public class SubExpression : BinaryExpression {
        public SubExpression(BaseExpression left, BaseExpression right)
            : base(left, right) {
        }

        public override long Evaluate() {
            return Left.Evaluate() - Right.Evaluate();
        }
    }
}
