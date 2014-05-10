using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32Imm16Opcode : BaseOpcode {

        protected BaseReg32Imm16Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var dest = parser.ReadReg32();
            parser.ReadToken(TokenType.Comma);
            var value = parser.ReadUshort();
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Destination32 = dest };
            output.EmitCode(translation.Opcode);
            output.EmitCode(value);
        }

    }
}
