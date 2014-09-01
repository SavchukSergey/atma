
namespace Atmega.Asm.Opcodes.Arithmetics {
    public class CpcOpcode : BaseReg32Reg32Opcode {

        public CpcOpcode()
            : base("000001rdddddrrrr") {
        }

        public override string ToString() {
            return string.Format("cpc {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }
    }
}
