namespace Atmega.Asm.Opcodes.Move {
    public class StsOpcode : BaseImm16Reg32Opcode {

        public StsOpcode()
            : base("1001001ddddd0000") {
        }

        public override string ToString() {
            return string.Format("sts {0}, {1}", FormatAddress(Address), FormatRegister(Source));
        }

    }
}
