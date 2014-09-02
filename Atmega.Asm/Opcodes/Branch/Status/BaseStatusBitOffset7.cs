using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Branch.Status {
    public abstract class BaseStatusBitOffset7 : BaseOpcode {
        
        protected BaseStatusBitOffset7(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var bit = parser.ReadBit();
            parser.ReadToken(TokenType.Comma);

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
            
            var translation = new OpcodeTranslation {Opcode = _opcodeTemplate, Offset7 = (sbyte) delta, BitNumber = bit};
            output.EmitCode(translation.Opcode);
        }
    }
}
