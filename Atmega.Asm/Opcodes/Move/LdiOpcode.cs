
namespace Atmega.Asm.Opcodes.Move {
    public class LdiOpcode : BaseReg16Imm8Opcode {

        public LdiOpcode()
            : base("1110KKKKddddKKKK") {
        }

        public override string ToString() {
            return string.Format("ldi {0}, {1}", FormatRegister(Register), Value);
        }
    }
}
