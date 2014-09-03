namespace Atmega.Asm.Opcodes.Branch {
    public class SbicOpcode : BasePort32BitOpcode {

        public SbicOpcode()
            : base("10011001pppppsss") {
        }

        public override string ToString() {
            return string.Format("sbic {0}, {1}", FormatPort(Port), FormatPortBit(Port, Bit));
        }

    }
}
