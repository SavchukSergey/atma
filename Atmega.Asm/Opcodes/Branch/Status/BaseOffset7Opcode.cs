using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Branch.Status {
    public abstract class BaseOffset7Opcode : BaseOpcode {

        protected BaseOffset7Opcode(string opcodeTemplate)
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
            if (delta > 63 || delta < -64) {
                throw new TokenException("relative jump out of range (-64; 63)", firstToken);
            }
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Offset7 = (sbyte)delta };
            output.EmitCode(translation.Opcode);
        }

        //public static BaseOffset7Opcode FromOpcode(ushort opcode) {
        //    var translation = new OpcodeTranslation { Opcode = opcode };
        //    var set = (translation.Opcode & 0x40) == 0;
        //    var bit = translation.BitNumber;
        //    if (set) {
        //        return new BrbsOpcode { Bit = bit, Offset = translation.Offset7 };
        //    }
        //    return new BrbcOpcode { Bit = bit, Offset = translation.Offset7 };
        //}
    }
}
