namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BaseStatusBitClearBranchOpcode : BrbcOpcode {

        private readonly byte _bit;

        public BaseStatusBitClearBranchOpcode(byte bit) {
            _bit = bit;
        }

        public override byte Bit { get { return _bit; } set { } }

        protected override void Parse(AsmParser parser) {
            ParseOffset(parser);
        }

    }
}
