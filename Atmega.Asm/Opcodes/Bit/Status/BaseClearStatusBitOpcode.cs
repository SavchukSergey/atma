namespace Atmega.Asm.Opcodes.Bit.Status {
    public class BaseClearStatusBitOpcode : BclrOpcode {

        private readonly byte _bit;

        public BaseClearStatusBitOpcode(byte bit) {
            _bit = bit;
        }

        public override byte Bit { get { return _bit; } set { } }

        protected override void Parse(AsmParser parser) {
        }
    }
}
