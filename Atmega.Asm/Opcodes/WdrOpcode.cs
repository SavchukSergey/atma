namespace Atmega.Asm.Opcodes {
    public class WdrOpcode : BaseSimpleOpcode {

        public WdrOpcode()
            : base("1001010110101000") {
        }

        public override string ToString() {
            return "wdr";
        }
    }
}
