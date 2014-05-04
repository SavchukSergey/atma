using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class ElpmOpcode : BaseOpcode {

        public ElpmOpcode()
            : base("1001010111011000") {
        }

        public override void Compile(AsmContext context) {
            if (context.Queue.IsEndOfLine) {
                context.EmitCode(_opcodeTemplate);
                return;
            }
            var translation = new OpcodeTranslation { Opcode = 0x9006 };
            var dest = context.Parser.ReadReg32();
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
