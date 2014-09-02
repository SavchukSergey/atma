namespace Atmega.Asm.Opcodes.Bit.Status {
    public class SetOpcode : BaseSimpleOpcode {

        public SetOpcode()
            : base("1001010001101000") {
        }

        public override string ToString() {
            return "set";
        }
    }
}
