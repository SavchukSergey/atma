
namespace Atmega.Asm.Opcodes.Arithmetics {
    public class SbcOpcode : BaseReg32Reg32Opcode {

        public SbcOpcode()
            : base("000010rdddddrrrr") {
        }

        public override string ToString() {
            return string.Format("sbc {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }

    }
}
