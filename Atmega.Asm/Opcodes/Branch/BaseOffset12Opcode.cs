using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Branch {
    public abstract class BaseOffset12Opcode : BaseOpcode {
        protected BaseOffset12Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var firstToken = context.Queue.Count > 0 ? context.Queue.Peek() : new Token();
            var offset = context.CalculateExpression();
            var currentOffset = context.Offset + 2;
            var delta = offset - currentOffset;
            if ((delta & 0x1) > 0) {
                throw new TokenException("invalid relative jump", firstToken);
            }
            delta /= 2;
            if (delta < -2048 || delta > 2047) {
                throw new TokenException("relative jump out of range (-2047; 2048)", firstToken);
            }
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            translation.Offset12 = (short)delta;
            context.EmitCode(translation.Opcode);
        }
    }
}
