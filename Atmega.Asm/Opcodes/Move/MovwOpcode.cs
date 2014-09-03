using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class MovwOpcode : BaseOpcode {

        public byte Destination { get; set; }

        public byte Register { get; set; }

        public MovwOpcode()
            : base("00000001ddddrrrr") {
        }

        protected override void Parse(AsmParser parser) {
            Destination = parser.ReadWordReg();
            parser.ReadToken(TokenType.Comma);
            Register = parser.ReadWordReg();

        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation {
                Opcode = _opcodeTemplate,
                Destination16Word = Destination,
                Register16Word = Register
            };
            output.EmitCode(translation.Opcode);
        }

        public override string ToString() {
            return string.Format("movw {0}, {1}", FormatRegister(Destination), FormatRegister(Register));
        }
    }
}
