
namespace Atmega.Asm.Opcodes.Arithmetics {
    public class SubiOpcode : BaseReg16Imm8Opcode {

        public SubiOpcode()
            : base("0101KKKKddddKKKK") {
        }

        public override string ToString() {
            return string.Format("subi {0}, {1}", FormatRegister(Register), Value);
        }
    }
}
