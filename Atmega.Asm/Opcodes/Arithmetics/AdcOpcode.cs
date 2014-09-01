
namespace Atmega.Asm.Opcodes.Arithmetics {
    public class AdcOpcode : BaseReg32Reg32Opcode {

        public AdcOpcode()
            : base("000111rdddddrrrr") {
        }

        public override string ToString() {
            return string.Format("adc {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }
     
    }
}
