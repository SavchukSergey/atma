
namespace Atmega.Asm.Opcodes.Arithmetics {
    public class SubOpcode : BaseReg32Reg32Opcode {

        public SubOpcode()
            : base("000110rdddddrrrr") {
        }

        public override string ToString() {
            return string.Format("sub {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }
    }
}
