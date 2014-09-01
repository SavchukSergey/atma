
namespace Atmega.Asm.Opcodes.Arithmetics {
    public class AddOpcode : BaseReg32Reg32Opcode {

        public AddOpcode()
            : base("000011rdddddrrrr") {
        }

        public override string ToString() {
            return string.Format("add {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }
    }
}
