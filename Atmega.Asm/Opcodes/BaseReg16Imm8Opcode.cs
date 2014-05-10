using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg16Imm8Opcode : BaseOpcode {

        protected BaseReg16Imm8Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = parser.ReadReg16();
            translation.Destination16 = dest;
            parser.ReadToken(TokenType.Comma);
            var value = parser.ReadByte();
            translation.Imm8 = value;
            output.EmitCode(translation.Opcode);
        }


    }
}
