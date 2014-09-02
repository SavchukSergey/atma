using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Branch {
    public abstract class BaseOffset12Opcode : BaseOpcode {

        public short Offset { get; set; }

        protected BaseOffset12Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            Token firstToken;
            var offset = parser.CalculateExpression(out firstToken);
            var zeroOffset = output.Offset + 2;
            var delta = offset - zeroOffset;
            if ((delta & 0x1) > 0) {
                throw new TokenException("invalid relative jump", firstToken);
            }
            delta /= 2;
            if (delta < -2048 || delta > 2047) {
                throw new TokenException("relative jump out of range (-2047; 2048)", firstToken);
            }
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Offset12 = (short)delta };
            output.EmitCode(translation.Opcode);
        }

    }
}
