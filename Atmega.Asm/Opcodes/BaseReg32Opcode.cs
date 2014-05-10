namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32Opcode : BaseOpcode {

        protected BaseReg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var dest = parser.ReadReg32();
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Destination32 = dest };
            output.EmitCode(translation.Opcode);
        }

    }
}
