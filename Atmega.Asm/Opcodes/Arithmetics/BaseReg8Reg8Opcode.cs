using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Arithmetics {
    public class BaseReg8Reg8Opcode : BaseOpcode {
        public BaseReg8Reg8Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = parser.ReadReg8();
            translation.Destination8 = dest;
            parser.ReadToken(TokenType.Comma);
            var reg = parser.ReadReg8();
            translation.Register8 = reg;
            output.EmitCode(translation.Opcode);
        }
    }
}
