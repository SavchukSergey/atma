namespace Atmega.Asm.Expressions {
    public abstract class UnaryExpression : BaseExpression {

        private readonly BaseExpression _inner;

        protected UnaryExpression(BaseExpression inner) {
            _inner = inner;
        }

        public BaseExpression Inner {
            get { return _inner; }
        }

    }
}
