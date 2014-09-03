namespace Atmega.Asm.Opcodes.Branch {
    public class SbrcOpcode : BaseReg32BitOpcode {

        public SbrcOpcode()
            : base("1111110rrrrr0sss") {
        }

        public override string ToString() {
            return string.Format("sbrc {0}, {1}", FormatRegister(Register), Bit);
        }
    }
}
