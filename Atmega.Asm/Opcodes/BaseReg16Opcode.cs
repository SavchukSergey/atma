namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg16Opcode : BaseOpcode {

        protected BaseReg16Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = parser.ReadReg16();
            translation.Destination16 = dest;
            output.EmitCode(translation.Opcode);
        }

    }
}
