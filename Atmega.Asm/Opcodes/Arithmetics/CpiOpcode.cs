
namespace Atmega.Asm.Opcodes.Arithmetics {
    public class CpiOpcode : BaseReg16Imm8Opcode {

        public CpiOpcode()
            : base("0011KKKKddddKKKK") {
        }

        public override string ToString() {
            return string.Format("cpi {0}, {1}", FormatRegister(Register), Value);
        }

    }
}
