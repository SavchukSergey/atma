namespace Atmega.Asm.Expressions {
    public class ShiftRightExpression : BinaryExpression {

        public ShiftRightExpression(BaseExpression left, BaseExpression right)
            : base(left, right) {
        }

        public override long Evaluate() {
            return Left.Evaluate() >> (int)Right.Evaluate();
        }
    }
}
