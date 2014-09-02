
namespace Atmega.Asm.Opcodes.Logic {
    public class OriOpcode : BaseReg16Imm8Opcode {

        public OriOpcode()
            : base("0110KKKKddddKKKK") {
        }

        public override string ToString() {
            return string.Format("ori {0}, {1}", FormatRegister(Register), Value);
        }
    }
}
