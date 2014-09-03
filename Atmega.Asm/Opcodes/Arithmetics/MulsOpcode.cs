
namespace Atmega.Asm.Opcodes.Arithmetics {
    public class MulsOpcode : BaseReg16Reg16Opcode {

        public MulsOpcode()
            : base("00000010ddddrrrr") {
        }

        public override string ToString() {
            return string.Format("muls {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }
    }
}
