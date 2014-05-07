using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class MovwOpcode : BaseOpcode {

        public MovwOpcode()
            : base("00000001ddddrrrr") {
        }

        public override void Compile(AsmContext context) {
            var target = ReadWordReg(context);
            context.Queue.Read(TokenType.Comma);
            var source = ReadWordReg(context);

            var translation = new OpcodeTranslation {
                Opcode = _opcodeTemplate,
                Destination16Word = target,
                Register16Word = source
            };
            context.EmitCode(translation.Opcode);
        }

        private byte ReadWordReg(AsmContext context) {
            var token = context.Queue.Peek();
            var high = context.Parser.ReadReg32();
            if ((high & 0x01) == 0) {
                throw new TokenException("expected odd register (r1, r3, ..., r29, r31)", token);
            }

            context.Queue.Read(TokenType.Colon);

            token = context.Queue.Peek();
            var low = context.Parser.ReadReg32();
            if (low != high - 1) {
                throw new TokenException("expected register r" + (high - 1), token);
            }

            return low;
        }
    }
}
