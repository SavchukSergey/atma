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

        protected abstract string OpcodeName { get; }

        protected string FormatOffset() {
            var delta = (Delta - 1) * 2;
            if (delta == 0) return "$";
            if (delta < 0) return "$" + delta;
            return "$+" + delta;
        }

        public override string ToString() {
            return string.Format("{0} {1}", OpcodeName, FormatOffset());
        }


        public static BaseStatusBitOffset7Opcode FromOpcode(ushort opcode) {
            var clr = (opcode & 0x0400) > 0;
            if (clr) return BaseStatusBitClearBranchOpcode.FromOpcode(opcode);
            return BaseStatusBitSetBranchOpcode.FromOpcode(opcode);
        }

    }
}
