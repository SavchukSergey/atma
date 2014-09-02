
namespace Atmega.Asm.Opcodes.Move {
    public class MovOpcode : BaseReg32Reg32Opcode {

        public MovOpcode()
            : base("001011rdddddrrrr") {
        }

        public override string ToString() {
            return string.Format("mov {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }
    }
}
