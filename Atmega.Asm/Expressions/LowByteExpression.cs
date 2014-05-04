namespace Atmega.Asm.Expressions {
    public class LowByteExpression : BaseExpression {
        private readonly BaseExpression _inner;

        public LowByteExpression(BaseExpression inner) {
            _inner = inner;
        }

        public override long Evaluate() {
            return _inner.Evaluate() & 0xff;
        }
    }
}
