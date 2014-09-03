namespace Atmega.Asm.Opcodes.Bit {
    public class CbiOpcode : BasePort32BitOpcode {
        
        public CbiOpcode()
            : base("10011000pppppsss") {
        }

        public override string ToString() {
            return string.Format("cbi {0}, {1}", FormatPort(Port), FormatPortBit(Port, Bit));
        }

    }
}
