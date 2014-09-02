
namespace Atmega.Asm.Opcodes.Arithmetics {
    public class SbciOpcode : BaseReg16Imm8Opcode {

        public SbciOpcode()
            : base("0100KKKKddddKKKK") {
        }

        public override string ToString() {
            return string.Format("sbci {0}, {1}", FormatRegister(Register), Value);
        }
     
    }
}
