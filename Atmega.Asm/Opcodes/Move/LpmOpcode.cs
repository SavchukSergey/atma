﻿using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class LpmOpcode : BaseOpcode {

        public LpmOpcode()
            : base("1001010111001000") {
        }

        public override void Compile(AsmContext context) {
            if (context.Queue.IsEndOfLine) {
                context.EmitCode(_opcodeTemplate);
                return;
            }
            var translation = new OpcodeTranslation { Opcode = 0x9004 };
            var dest = context.ReadReg32();
            translation.Destination32 = dest;

            context.Queue.Read(TokenType.Comma);
            var zReg = context.Queue.Read(TokenType.Literal);
            if (zReg.StringValue.ToLower() != "z") throw new TokenException("Z register expected", zReg);

            if (!context.Queue.IsEndOfLine) {
                context.Queue.Read(TokenType.Plus);
                translation.Increment = true;
            }

            context.EmitCode(translation.Opcode);
        }
    }
}