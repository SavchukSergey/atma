namespace Atmega.Asm.Opcodes.Bit.Status {
    public class BaseSetStatusBitOpcode : BsetOpcode {

        private readonly byte _bit;

        public BaseSetStatusBitOpcode(byte bit) {
            _bit = bit;
        }

        public override byte Bit { get { return _bit; } set { } }

        protected override void Parse(AsmParser parser) {
        }

        public static new BsetOpcode FromOpcode(ushort opcode) {
            var translation = new OpcodeTranslation {
                Opcode = opcode
            };
            var bit = translation.StatusBitNumber;
            switch (bit) {
                case 0: return new SecOpcode();
                case 1: return new SezOpcode();
                case 2: return new SenOpcode();
                case 3: return new SevOpcode();
                case 4: return new SesOpcode();
                case 5: return new SehOpcode();
                case 6: return new SetOpcode();
                case 7: return new SeiOpcode();
                default:
                    return new BsetOpcode { Bit = bit };
            }
        }
    }
}
