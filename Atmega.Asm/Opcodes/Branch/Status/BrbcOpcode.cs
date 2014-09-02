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

    }
}
