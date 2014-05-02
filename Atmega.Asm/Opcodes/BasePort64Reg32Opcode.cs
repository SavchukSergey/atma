using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BasePort64Reg32Opcode : BaseOpcode {

        protected BasePort64Reg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var value = context.ReadPort64();
            translation.Port64 = value;
            context.Queue.Read(TokenType.Comma);
            var dest = context.ReadReg32();
            translation.Destination32 = dest;
            context.EmitCode(translation.Opcode);
        }

    }
}
