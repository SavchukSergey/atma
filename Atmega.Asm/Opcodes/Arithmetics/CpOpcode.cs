
namespace Atmega.Asm.Opcodes.Arithmetics {
    public class CpOpcode : BaseReg32Reg32Opcode {

        public CpOpcode()
            : base("000101rdddddrrrr") {
        }

        public override string ToString() {
            return string.Format("cp {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }
     
    }
}
