namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32SelfOpcode : BaseOpcode {

        protected BaseReg32SelfOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var reg = parser.ReadReg32();
            translation.Destination32 = reg;
            translation.Register32 = reg;
            output.EmitCode(translation.Opcode);
        }

    }
}
