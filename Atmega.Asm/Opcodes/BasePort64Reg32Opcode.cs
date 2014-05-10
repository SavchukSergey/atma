using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BasePort64Reg32Opcode : BaseOpcode {

        protected BasePort64Reg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var value = parser.ReadPort64();
            translation.Port64 = value;
            parser.ReadToken(TokenType.Comma);
            var dest = parser.ReadReg32();
            translation.Destination32 = dest;
            output.EmitCode(translation.Opcode);
        }

    }
}
