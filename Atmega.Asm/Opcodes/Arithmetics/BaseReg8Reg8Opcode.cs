using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Arithmetics {
    public class BaseReg8Reg8Opcode : BaseOpcode {
        public BaseReg8Reg8Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.ReadReg8();
            translation.Destination8 = dest;
            context.Queue.Read(TokenType.Comma);
            var reg = context.ReadReg8();
            translation.Register8 = reg;
            context.EmitCode(translation.Opcode);
        }
    }
}
