namespace Atmega.Asm.Opcodes {
    public class BaseMultiReg32Opcode : BaseOpcode {

        protected BaseMultiReg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            do {
                var dest = parser.ReadReg32();
                var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Destination32 = dest };
                output.EmitCode(translation.Opcode);
            } while (!parser.IsEndOfLine);
        }

    }
}
