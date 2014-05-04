namespace Atmega.Asm.Expressions {
    public class HighByteExpression : BaseExpression {
        private readonly BaseExpression _inner;

        public HighByteExpression(BaseExpression inner) {
            _inner = inner;
        }

        public override long Evaluate() {
            return (_inner.Evaluate() >> 8) & 0xff;
        }
    }
}
