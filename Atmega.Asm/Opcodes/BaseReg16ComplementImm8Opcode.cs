using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg16ComplementImm8Opcode : BaseOpcode {

        protected BaseReg16ComplementImm8Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var dest = parser.ReadReg16();
            parser.ReadToken(TokenType.Comma);
            var value = parser.ReadByte();
            var translation = new OpcodeTranslation {
                Opcode = _opcodeTemplate,
                Destination16 = dest,
                Imm8 = (byte)(255 - value)
            };
            output.EmitCode(translation.Opcode);
        }


    }
}
