using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class SpmOpcode : BaseOpcode {

        public bool PostIncrement { get; set; }

        public SpmOpcode()
            : base("1001010111101000") {
        }

        protected override void Parse(AsmParser parser) {
            if (parser.IsEndOfLine) {
                PostIncrement = false;
            } else {
                var zReg = parser.ReadToken(TokenType.Literal);
                if (zReg.StringValue.ToLower() != "z") throw new TokenException("Z+ expected", zReg);
                parser.ReadToken(TokenType.Plus);
                PostIncrement = true;
            }
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation {
                Opcode = _opcodeTemplate,
                SpmIncrement = PostIncrement
            };
            output.EmitCode(translation.Opcode);
        }

        public override string ToString() {
            if (PostIncrement) return "spm z+";
            return "spm";
        }
    }
}
