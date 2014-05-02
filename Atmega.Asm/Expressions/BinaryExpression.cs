namespace Atmega.Asm.Expressions {
    public abstract class BinaryExpression : BaseExpression {

        private readonly BaseExpression _left;
        private readonly BaseExpression _right;

        protected BinaryExpression(BaseExpression left, BaseExpression right) {
            _left = left;
            _right = right;
        }

        public BaseExpression Left {
            get { return _left; }
        }

        public BaseExpression Right {
            get { return _right; }
        }
    }
}
