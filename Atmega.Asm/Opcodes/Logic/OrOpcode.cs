
namespace Atmega.Asm.Opcodes.Logic {
    public class OrOpcode : BaseReg32Reg32Opcode {

        public OrOpcode()
            : base("001010rdddddrrrr") {
        }

        public override string ToString() {
            return string.Format("or {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }
     
    }
}
