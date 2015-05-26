using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Branch.Status {
    public abstract class BaseStatusBitOffset7Opcode : BaseOpcode {

        protected BaseStatusBitOffset7Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public virtual byte Bit { get; set; }

        private short _delta;
        public short Delta {
            get { return _delta; }
            set {
                if ((value & 0x1) > 0) {
                    throw new InvalidOperationException("invalid relative jump");
                }
                value >>= 1;
                if (value < -64 || value > 63) {
                    throw new InvalidOperationException("jump beyond 2048 boundary");
                }
                value <<= 1;
                _delta = value;
            }
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Offset7 = (sbyte)(Delta / 2), BitNumber = Bit };
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
            delta *= 2;
            Delta = (short)delta;
        }

        protected abstract string OpcodeName { get; }

        protected string FormatOffset() {
            return FormatOffset(Delta, 1);
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
