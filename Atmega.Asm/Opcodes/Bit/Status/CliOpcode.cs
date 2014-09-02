namespace Atmega.Asm.Opcodes.Bit.Status {
    public class CliOpcode : BaseSimpleOpcode {

        public CliOpcode()
            : base("1001010011111000") {
        }

        public override string ToString() {
            return "cli";
        }
    }
}
