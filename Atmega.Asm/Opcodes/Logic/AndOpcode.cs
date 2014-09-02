
namespace Atmega.Asm.Opcodes.Logic {
    public class AndOpcode : BaseReg32Reg32Opcode {

        public AndOpcode()
            : base("001000rdddddrrrr") {
        }

        public override string ToString() {
            if (Register == Destination) return string.Format("tst {0}", FormatRegister(Register));
            return string.Format("and {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }
    }
}
