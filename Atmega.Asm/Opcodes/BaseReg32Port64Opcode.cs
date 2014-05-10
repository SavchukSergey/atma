using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32Port64Opcode : BaseOpcode {

        protected BaseReg32Port64Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var dest = parser.ReadReg32();
            parser.ReadToken(TokenType.Comma);
            var value = parser.ReadPort64();
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Destination32 = dest, Port64 = value };
            output.EmitCode(translation.Opcode);
        }

    }
}
