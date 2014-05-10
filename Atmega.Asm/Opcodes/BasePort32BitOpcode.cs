using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public class BasePort32BitOpcode : BaseOpcode {
        public BasePort32BitOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var dest = parser.ReadPort32();
            parser.ReadToken(TokenType.Comma);
            var value = parser.ReadBit();

            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Port32 = dest, BitNumber = value };
            output.EmitCode(translation.Opcode);
        }
    }
}
