
namespace Atmega.Asm.Opcodes.Logic {
    public class EorOpcode : BaseReg32Reg32Opcode {

        public EorOpcode()
            : base("001001rdddddrrrr") {
        }

        public override string ToString() {
            if (Register == Destination) {
                return string.Format("clr {0}", FormatRegister(Register));
            }
            return string.Format("eor {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }
    }
}
