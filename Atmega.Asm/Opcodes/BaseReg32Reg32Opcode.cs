using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32Reg32Opcode : BaseOpcode {

        protected BaseReg32Reg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.ReadReg32();
            translation.Destination32 = dest;
            context.Queue.Read(TokenType.Comma);
            var reg = context.ReadReg32();
            translation.Register32 = reg;
            context.EmitCode(translation.Opcode);
        }


    }
}
