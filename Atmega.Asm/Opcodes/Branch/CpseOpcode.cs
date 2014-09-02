
namespace Atmega.Asm.Opcodes.Branch {
    public class CpseOpcode : BaseReg32Reg32Opcode {

        public CpseOpcode()
            : base("000100rdddddrrrr") {
        }

        public override string ToString() {
            return string.Format("cpse {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }
    }
}
