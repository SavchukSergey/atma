namespace Atmega.Asm.Opcodes.Bit.Status {
    public class CltOpcode : BaseSimpleOpcode {

        public CltOpcode()
            : base("1001010011101000") {
        }

        public override string ToString() {
            return "clt";
        }
    }
}
