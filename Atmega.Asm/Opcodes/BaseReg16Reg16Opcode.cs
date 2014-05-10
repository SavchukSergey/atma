using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg16Reg16Opcode : BaseOpcode {

        protected BaseReg16Reg16Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var dest = parser.ReadReg16();
            parser.ReadToken(TokenType.Comma);
            var reg = parser.ReadReg16();

            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Destination16 = dest, Register16 = reg };
            output.EmitCode(translation.Opcode);
        }

    }
}
