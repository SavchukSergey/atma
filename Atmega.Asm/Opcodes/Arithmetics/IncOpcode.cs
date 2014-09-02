namespace Atmega.Asm.Opcodes.Arithmetics {
    public class IncOpcode: BaseReg32Opcode {

        public IncOpcode()
            : base("1001010rrrrr0011") {
        }

        public override string ToString() {
            return string.Format("inc {0}", FormatRegister(Register));
        }
    }
}
