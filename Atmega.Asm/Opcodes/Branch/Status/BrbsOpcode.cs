using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrbsOpcode : BaseStatusBitOffset7Opcode {

        public BrbsOpcode()
            : base("111100lllllllsss") {
        }

        protected override void Parse(AsmParser parser) {
            Bit = parser.ReadBit();
            parser.ReadToken(TokenType.Comma);
            ParseOffset(parser);
        }

        protected override string OpcodeName {
            get { return "brbs"; }
        }

        public static new BrbsOpcode FromOpcode(ushort opcode) {
            var translation = new OpcodeTranslation { Opcode = opcode };
            var bit = translation.BitNumber;
            var delta = (short)(translation.Offset7 * 2);
            switch (bit) {
                case 0: return new BrcsOpcode { Delta = delta };
                case 1: return new BreqOpcode { Delta = delta };
                case 2: return new BrmiOpcode { Delta = delta };
                case 3: return new BrvsOpcode { Delta = delta };
                case 4: return new BrltOpcode { Delta = delta };
                case 5: return new BrhsOpcode { Delta = delta };
                case 6: return new BrtsOpcode { Delta = delta };
                case 7: return new BrieOpcode { Delta = delta };
                default:
                    return new BrbsOpcode { Bit = bit, Delta = delta };
            }
        }
    }
}
