using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg16Reg16Opcode : BaseOpcode {

        protected BaseReg16Reg16Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.ReadReg16();
            translation.Destination16 = dest;
            context.Queue.Read(TokenType.Comma);
            var reg = context.ReadReg16();
            translation.Register16 = reg;
            context.EmitCode(translation.Opcode);
        }

    }
}
