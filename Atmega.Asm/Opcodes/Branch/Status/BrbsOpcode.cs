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

    }
}
