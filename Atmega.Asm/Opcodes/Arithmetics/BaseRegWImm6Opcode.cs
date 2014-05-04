using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Arithmetics {
    public class BaseRegWImm6Opcode : BaseOpcode {
        public BaseRegWImm6Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.Parser.ReadRegW24();
            translation.RegW24 = dest;
            context.Queue.Read(TokenType.Comma);
            Token exprToken;
            var imm = context.Parser.CalculateExpression(out exprToken);
            if (imm > 63) {
                throw new TokenException("value must be less than 64", exprToken);
            }
            translation.Imm6 = (byte)imm;
            context.EmitCode(translation.Opcode);
        }
    }
}
