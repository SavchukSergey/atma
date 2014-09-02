namespace Atmega.Asm.Opcodes.Bit {
    public class BclrOpcode : BaseStatusBitOpcode {
        public BclrOpcode()
            : base("100101001SSS1000") {
        }

        public override string ToString() {
            return string.Format("bclr {0}", FormatStatusBit(Bit));
        }
    }
}
