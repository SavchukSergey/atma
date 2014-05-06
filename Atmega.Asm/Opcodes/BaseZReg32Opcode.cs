using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseZReg32Opcode : BaseOpcode {

        protected BaseZReg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var zToken = context.Parser.ReadToken(TokenType.Literal);
            if (zToken.StringValue.ToLower() != "z") throw new TokenException("Z register expected", zToken);
            context.Parser.ReadToken(TokenType.Comma);
            var dest = context.Parser.ReadReg32();
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            translation.Destination32 = dest;
            context.EmitCode(translation.Opcode);
        }

    }
}
