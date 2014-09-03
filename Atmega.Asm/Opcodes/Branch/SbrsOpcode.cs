namespace Atmega.Asm.Opcodes.Branch {
    public class SbrsOpcode : BaseReg32BitOpcode {

        public SbrsOpcode()
            : base("1111111rrrrr0sss") {
        }

        public override string ToString() {
            return string.Format("sbrs {0}, {1}", FormatRegister(Register), Bit);
        }

    }
}
