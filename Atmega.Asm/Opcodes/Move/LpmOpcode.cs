using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class LpmOpcode : BaseOpcode {

        public LpmOpcode()
            : base("1001010111001000") {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            if (parser.IsEndOfLine) {
                output.EmitCode(_opcodeTemplate);
                return;
            }
            var dest = parser.ReadReg32();

            parser.ReadToken(TokenType.Comma);
            var zReg = parser.ReadToken(TokenType.Literal);
            if (zReg.StringValue.ToLower() != "z") throw new TokenException("Z register expected", zReg);

            var increment = false;
            if (!parser.IsEndOfLine) {
                parser.ReadToken(TokenType.Plus);
                increment = true;
            }

            var translation = new OpcodeTranslation { Opcode = 0x9004, Destination32 = dest, Increment = increment };
            output.EmitCode(translation.Opcode);
        }
    }
}
