namespace Atmega.Asm.Opcodes.Bit {
    public class BstOpcode : BaseReg32BitOpcode {

        public BstOpcode()
            : base("1111101ddddd0sss") {
        }

        public override string ToString() {
            return string.Format("bst {0}, {1}", FormatRegister(Register), Bit);
        }
    }
}
