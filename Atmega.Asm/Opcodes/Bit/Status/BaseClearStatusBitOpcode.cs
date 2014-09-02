namespace Atmega.Asm.Opcodes.Bit.Status {
    public class BaseClearStatusBitOpcode : BclrOpcode {

        private readonly byte _bit;

        public BaseClearStatusBitOpcode(byte bit) {
            _bit = bit;
        }

        public override byte Bit { get { return _bit; } set { } }

        protected override void Parse(AsmParser parser) {
        }

        public static new BclrOpcode FromOpcode(ushort opcode) {
            var translation = new OpcodeTranslation {
                Opcode = opcode
            };
            var bit = translation.StatusBitNumber;
            switch (bit) {
                case 0: return new ClcOpcode();
                case 1: return new ClzOpcode();
                case 2: return new ClnOpcode();
                case 3: return new ClvOpcode();
                case 4: return new ClsOpcode();
                case 5: return new ClhOpcode();
                case 6: return new CltOpcode();
                case 7: return new CliOpcode();
                default:
                    return new BclrOpcode { Bit = bit };
            }
        }
    }
}
