namespace Atmega.Asm.Expressions {
    public class NegateExpression : UnaryExpression {
        public NegateExpression(BaseExpression inner)
            : base(inner) {
        }

        public override long Evaluate() {
            return -Inner.Evaluate();
        }
    }
}
