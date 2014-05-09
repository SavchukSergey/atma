using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class SpmOpcode : BaseOpcode {

        private static readonly ushort _postIncrementTemplate = ParseOpcodeTemplate("1001010111111000");

        public SpmOpcode()
            : base("1001010111101000") {
        }

        public override void Compile(AsmContext context) {
            if (context.Queue.IsEndOfLine) {
                context.EmitCode(_opcodeTemplate);
            } else {
                var zReg = context.Parser.ReadToken(TokenType.Literal);
                if (zReg.StringValue.ToLower() != "z") throw new TokenException("Z+ expected", zReg);
                context.Parser.ReadToken(TokenType.Plus);
                context.EmitCode(_postIncrementTemplate);
            }
        }

    }
}
