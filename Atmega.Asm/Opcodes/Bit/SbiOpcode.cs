namespace Atmega.Asm.Opcodes.Bit {
    public class SbiOpcode : BasePort32BitOpcode {

        public SbiOpcode()
            : base("10011010pppppsss") {
        }

        public override string ToString() {
            return string.Format("sbi {0}, {1}", FormatPort(Port), FormatPortBit(Port, Bit));
        }
    }
}
