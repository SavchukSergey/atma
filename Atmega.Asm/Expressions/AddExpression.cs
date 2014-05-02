namespace Atmega.Asm.Expressions {
    public class AddExpression : BinaryExpression {
        
        public AddExpression(BaseExpression left, BaseExpression right)
            : base(left, right) {
        }

        public override long Evaluate() {
            return Left.Evaluate() + Right.Evaluate();
        }
    }
}
