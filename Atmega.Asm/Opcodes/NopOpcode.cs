namespace Atmega.Asm.Opcodes {
    public class NopOpcode : BaseSimpleOpcode {
        
        public NopOpcode()
            : base("0000000000000000") {
        }

        public override string ToString() {
            return "nop";
        }

    }
}
