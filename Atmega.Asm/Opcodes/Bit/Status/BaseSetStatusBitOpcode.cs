namespace Atmega.Asm.Opcodes.Bit.Status {
    public class BaseSetStatusBitOpcode : BsetOpcode {

        private readonly byte _bit;

        public BaseSetStatusBitOpcode(byte bit) {
            _bit = bit;
        }

        public override byte Bit { get { return _bit; } set { } }

        protected override void Parse(AsmParser parser) {
        }
    }
}
