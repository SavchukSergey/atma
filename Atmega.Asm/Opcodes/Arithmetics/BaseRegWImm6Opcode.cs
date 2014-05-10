using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Arithmetics {
    public class BaseRegWImm6Opcode : BaseOpcode {
        public BaseRegWImm6Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var dest = parser.ReadRegW24();
            parser.ReadToken(TokenType.Comma);
            Token exprToken;
            var imm = parser.CalculateExpression(out exprToken);
            if (imm > 63) {
                throw new TokenException("value must be less than 64", exprToken);
            }
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, RegW24 = dest, Imm6 = (byte)imm };
            output.EmitCode(translation.Opcode);
        }
    }
}
