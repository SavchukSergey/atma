using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public class BaseReg32BitOpcode : BaseOpcode {
        public BaseReg32BitOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var dest = parser.ReadReg32();
            parser.ReadToken(TokenType.Comma);
            var value = parser.ReadBit();

            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Destination32 = dest, BitNumber = value };
            output.EmitCode(translation.Opcode);
        }
    }
}
