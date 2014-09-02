namespace Atmega.Asm.Opcodes.Bit {
    public class BldOpcode : BaseReg32BitOpcode {

        public BldOpcode()
            : base("1111100ddddd0sss") {
        }

        public override string ToString() {
            return string.Format("bld {0}, {1}", FormatRegister(Register), Bit);
        }

    }
}
