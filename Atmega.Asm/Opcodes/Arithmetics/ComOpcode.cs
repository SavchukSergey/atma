namespace Atmega.Asm.Opcodes.Arithmetics {
    public class ComOpcode : BaseReg32Opcode {

        public ComOpcode()
            : base("1001010rrrrr0000") {
        }

        public override string ToString() {
            return string.Format("com {0}", FormatRegister(Register));
        }
    }
}
