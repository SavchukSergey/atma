using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class SpmOpcode : BaseOpcode {

        private static readonly ushort _postIncrementTemplate = ParseOpcodeTemplate("1001010111111000");

        public SpmOpcode()
            : base("1001010111101000") {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            if (parser.IsEndOfLine) {
                output.EmitCode(_opcodeTemplate);
            } else {
                var zReg = parser.ReadToken(TokenType.Literal);
                if (zReg.StringValue.ToLower() != "z") throw new TokenException("Z+ expected", zReg);
                parser.ReadToken(TokenType.Plus);
                output.EmitCode(_postIncrementTemplate);
            }
        }

    }
}
