using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Arithmetics {
    public class BaseRegWImm6Opcode : BaseOpcode {
        public BaseRegWImm6Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.ReadRegW24();
            translation.RegW24 = dest;
            context.Queue.Read(TokenType.Comma);
            var imm = context.CalculateExpression();
            if (imm > 63) {
                throw new Exception("value must be less than 64");
            }
            translation.Imm6 = (byte)imm;
            context.EmitCode(translation.Opcode);
        }
    }
}
