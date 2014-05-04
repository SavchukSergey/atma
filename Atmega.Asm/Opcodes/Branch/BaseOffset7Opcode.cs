using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Branch {
    public abstract class BaseOffset7Opcode : BaseOpcode {
        
        protected BaseOffset7Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            Token firstToken;
            var offset = context.Parser.CalculateExpression(out firstToken);
            var currentOffset = context.Offset + 2;
            var delta = offset - currentOffset;
            if ((delta & 0x1) > 0) {
                throw new TokenException("invalid relative jump", firstToken);
            }
            delta /= 2;
            if (delta > 63 || delta < -64) {
                throw new TokenException("relative jump out of range (-64; 63)", firstToken);
            }
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            translation.Offset7 = (sbyte)delta;
            context.EmitCode(translation.Opcode);
        }
    }
}
