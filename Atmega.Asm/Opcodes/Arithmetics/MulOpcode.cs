
namespace Atmega.Asm.Opcodes.Arithmetics {
    public class MulOpcode : BaseReg32Reg32Opcode {

        public MulOpcode()
            : base("100111rdddddrrrr") {
        }

        public override string ToString() {
            return string.Format("mul {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }

    }
}
