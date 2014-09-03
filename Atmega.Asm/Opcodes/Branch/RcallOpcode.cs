namespace Atmega.Asm.Opcodes.Branch {
    public class RcallOpcode : BaseOffset12Opcode {

        public RcallOpcode()
            : base("1101LLLLLLLLLLLL") {
        }

        public override string ToString() {
            return string.Format("rcall {0}", FormatOffset(Delta, 1));
        }
    }
}
