namespace Atmega.Asm.Opcodes.Bit {
    public class BsetOpcode : BaseStatusBitOpcode {
        public BsetOpcode()
            : base("100101000SSS1000") {
        }

        public override string ToString() {
            return string.Format("bset {0}", FormatStatusBit(Bit));
        }

    }
}
