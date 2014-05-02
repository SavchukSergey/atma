namespace Atmega.Asm.Expressions {
    public class ShiftLeftExpression : BinaryExpression {

        public ShiftLeftExpression(BaseExpression left, BaseExpression right)
            : base(left, right) {
        }

        public override long Evaluate() {
            return Left.Evaluate() << (int)Right.Evaluate();
        }
    }
}
