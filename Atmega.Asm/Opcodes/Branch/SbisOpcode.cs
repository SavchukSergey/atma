namespace Atmega.Asm.Opcodes.Branch {
    public class SbisOpcode : BasePort32BitOpcode {

        public SbisOpcode()
            : base("10011011pppppsss") {
        }

        public override string ToString() {
            return string.Format("sbis {0}, {1}", FormatPort(Port), FormatPortBit(Port, Bit));
        }

    }
}
