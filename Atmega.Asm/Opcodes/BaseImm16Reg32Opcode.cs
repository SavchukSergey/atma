using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseImm16Reg32Opcode : BaseOpcode {

        protected BaseImm16Reg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var value = parser.ReadUshort();
            parser.ReadToken(TokenType.Comma);
            var dest = parser.ReadReg32();

            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Destination32 = dest };
            output.EmitCode(translation.Opcode);
            output.EmitCode(value);
        }

    }
}
