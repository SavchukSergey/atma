using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class LpmOpcode : BaseOpcode {

        public LpmOpcode()
            : base("1001010111001000") {
        }

        public byte Destination { get; set; }

        public bool Increment { get; set; }

        public bool NoArgs { get; set; }

        protected override void Parse(AsmParser parser) {
            if (parser.IsEndOfLine) {
                Destination = 0;
                Increment = false;
                NoArgs = true;
                return;
            }
            NoArgs = false;
            Destination = parser.ReadReg32();

            parser.ReadToken(TokenType.Comma);
            var zReg = parser.ReadToken(TokenType.Literal);
            if (zReg.StringValue.ToLower() != "z") throw new TokenException("Z register expected", zReg);

            Increment = false;
            if (!parser.IsEndOfLine) {
                parser.ReadToken(TokenType.Plus);
                Increment = true;
            }
        }

        protected override void Compile(AsmSection output) {
            if (NoArgs) {
                output.EmitCode(0x95c8);
            } else {
                var translation = new OpcodeTranslation { Opcode = 0x9004, Destination32 = Destination, Increment = Increment };
                output.EmitCode(translation.Opcode);
            }
        }

        public override string ToString() {
            if (NoArgs) {
                return "lpm";
            }
            return string.Format("lpm {0}, z{1}", FormatRegister(Destination), Increment ? "+" : "");
        }
    }
}
