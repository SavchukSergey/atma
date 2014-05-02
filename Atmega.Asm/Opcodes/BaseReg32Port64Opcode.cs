using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32Port64Opcode : BaseOpcode {

        protected BaseReg32Port64Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.ReadReg32();
            translation.Destination32 = dest;
            context.Queue.Read(TokenType.Comma);
            var value = context.ReadPort64();
            translation.Port64 = value;
            context.EmitCode(translation.Opcode);
        }


    }
}
