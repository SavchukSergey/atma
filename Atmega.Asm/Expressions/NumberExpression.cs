namespace Atmega.Asm.Expressions {
    public class NumberExpression : BaseExpression {

        public long Value { get; set; }


        public override long Evaluate() {
            return Value;
        }
    }
}
