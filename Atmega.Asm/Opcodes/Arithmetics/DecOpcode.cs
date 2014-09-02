namespace Atmega.Asm.Opcodes.Arithmetics {
    public class DecOpcode: BaseReg32Opcode {

        public DecOpcode()
            : base("1001010rrrrr1010") {
        }

        public override string ToString() {
            return string.Format("dec {0}", FormatRegister(Register));
        }
    }
}
