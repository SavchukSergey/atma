namespace Atmega.Asm.Expressions {
    public class ModExpression : BinaryExpression {

        public ModExpression(BaseExpression left, BaseExpression right)
            : base(left, right) {
        }

        public override long Evaluate() {
            return Left.Evaluate() % Right.Evaluate();
        }
    }
}
