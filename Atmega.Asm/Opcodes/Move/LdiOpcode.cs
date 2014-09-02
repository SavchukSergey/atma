
namespace Atmega.Asm.Opcodes.Move {
    public class LdiOpcode : BaseReg16Imm8Opcode {

        public LdiOpcode()
            : base("1110KKKKddddKKKK") {
        }

        public override string ToString() {
            if (Value == 255) {
                return string.Format("ser {0}", FormatRegister(Register));
            }
            return string.Format("ldi {0}, {1}", FormatRegister(Register), Value);
        }
    }
}
