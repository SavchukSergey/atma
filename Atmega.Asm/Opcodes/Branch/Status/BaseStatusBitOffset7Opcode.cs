using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Branch.Status {
    public abstract class BaseStatusBitOffset7Opcode : BaseOpcode {

        protected BaseStatusBitOffset7Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public virtual byte Bit { get; set; }

        public short Delta { get; set; }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Offset7 = (sbyte)Delta, BitNumber = Bit };
            output.EmitCode(translation.Opcode);
        }

        protected void ParseOffset(AsmParser parser) {
            Token firstToken;
            var offset = parser.CalculateExpression(out firstToken);
            var zeroOffset = parser.CurrentOffset + 2;
            var delta = offset - zeroOffset;
            if ((delta & 0x1) > 0) {
                throw new TokenException("invalid relative jump", firstToken);
            }
            delta /= 2;
            if (delta > 63 || delta < -64) {
                throw new TokenException("relative jump out of range (-64; 63)", firstToken);
            }

            Delta = (short)delta;
        }
    }
}
