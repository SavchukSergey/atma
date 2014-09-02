
namespace Atmega.Asm.Opcodes.Logic {
    public class AndiOpcode : BaseReg16Imm8Opcode {

        public AndiOpcode()
            : base("0111KKKKddddKKKK") {
        }

        public override string ToString() {
            return string.Format("andi {0}, {1}", FormatRegister(Register), Value);
        }
     
    }
}
