namespace Atmega.Asm.Opcodes.Bit {
    public abstract class BaseStatusBitOpcode : BaseOpcode {

        protected BaseStatusBitOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var value = parser.ReadBit();
            translation.StatusBitNumber = value;
            output.EmitCode(translation.Opcode);
        }
    }
}
