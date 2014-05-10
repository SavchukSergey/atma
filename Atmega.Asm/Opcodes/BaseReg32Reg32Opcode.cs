using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32Reg32Opcode : BaseOpcode {

        protected BaseReg32Reg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var dest = parser.ReadReg32();
            parser.ReadToken(TokenType.Comma);
            var reg = parser.ReadReg32();

            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Destination32 = dest, Register32 = reg };
            output.EmitCode(translation.Opcode);
        }

    }
}
