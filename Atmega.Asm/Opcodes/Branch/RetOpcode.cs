namespace Atmega.Asm.Opcodes.Branch {
    public class RetOpcode : BaseSimpleOpcode {

        public RetOpcode()
            : base("1001010100001000") {
        }

        public override string ToString() {
            return "ret";
        }

    }
}
