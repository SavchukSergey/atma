using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class MovwOpcode : BaseOpcode {

        public MovwOpcode()
            : base("00000001ddddrrrr") {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var target = parser.ReadWordReg();
            parser.ReadToken(TokenType.Comma);
            var source = parser.ReadWordReg();

            var translation = new OpcodeTranslation {
                Opcode = _opcodeTemplate,
                Destination16Word = target,
                Register16Word = source
            };
            output.EmitCode(translation.Opcode);
        }

    }
}
