namespace Atmega.Asm.Opcodes.Move {
    public class SwapOpcode : BaseReg32Opcode {

        public SwapOpcode()
            : base("1001010rrrrr0010") {
        }

        public override string ToString() {
            return string.Format("swap {0}", FormatRegister(Register));
        }
    }
}
