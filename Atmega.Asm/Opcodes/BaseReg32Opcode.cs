namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32Opcode : BaseOpcode {

        protected BaseReg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.ReadReg32();
            translation.Destination32 = dest;
            context.EmitCode(translation.Opcode);
        }

    }
}
