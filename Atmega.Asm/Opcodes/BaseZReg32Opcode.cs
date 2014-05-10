using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseZReg32Opcode : BaseOpcode {

        protected BaseZReg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var zToken = parser.ReadToken(TokenType.Literal);
            if (zToken.StringValue.ToLower() != "z") throw new TokenException("Z register expected", zToken);
            parser.ReadToken(TokenType.Comma);
            var dest = parser.ReadReg32();
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Destination32 = dest };
            output.EmitCode(translation.Opcode);
        }

    }
}
