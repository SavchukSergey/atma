namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BaseStatusBitSetBranchOpcode : BrbsOpcode {

        private readonly byte _bit;

        public BaseStatusBitSetBranchOpcode(byte bit) {
            _bit = bit;
        }

        public override byte Bit { get { return _bit; } set { } }

        protected override void Parse(AsmParser parser) {
            ParseOffset(parser);
        }
    }
}
