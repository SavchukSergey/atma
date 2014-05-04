namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32SelfOpcode : BaseOpcode {

        protected BaseReg32SelfOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var reg = context.Parser.ReadReg32();
            translation.Destination32 = reg;
            translation.Register32 = reg;
            context.EmitCode(translation.Opcode);
        }

    }
}
