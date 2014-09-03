namespace Atmega.Asm.Opcodes.Bit {
    public class LsrOpcode : BaseReg32Opcode {

        public LsrOpcode()
            : base("1001010rrrrr0110") {
        }

        public override string ToString() {
            return string.Format("lsr {0}", FormatRegister(Register));
        }
    }
}
