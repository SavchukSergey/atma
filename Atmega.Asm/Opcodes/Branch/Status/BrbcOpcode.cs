using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrbcOpcode : BaseStatusBitOffset7Opcode {
       
        public BrbcOpcode()
            : base("111101lllllllsss") {
        }

        protected override void Parse(AsmParser parser) {
            Bit = parser.ReadBit();
            parser.ReadToken(TokenType.Comma);
            ParseOffset(parser);
        }

        protected override string OpcodeName {
            get { return "brbc"; }
        }

        public static new BrbcOpcode FromOpcode(ushort opcode) {
            var translation = new OpcodeTranslation { Opcode = opcode };
            var bit = translation.BitNumber;
            var delta = translation.Offset7;
            switch (bit) {
                case 0: return new BrccOpcode { Delta = delta };
                case 1: return new BrneOpcode { Delta = delta };
                case 2: return new BrplOpcode { Delta = delta };
                case 3: return new BrvcOpcode { Delta = delta };
                case 4: return new BrgeOpcode { Delta = delta };
                case 5: return new BrhcOpcode { Delta = delta };
                case 6: return new BrtcOpcode { Delta = delta };
                case 7: return new BridOpcode { Delta = delta };
                default:
                    return new BrbcOpcode { Bit = bit, Delta = delta };
            }
        }
    }
}
