namespace Atmega.Asm.Opcodes.Bit {
    public abstract class BaseStatusBitOpcode : BaseOpcode {

        protected BaseStatusBitOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var value = context.ReadBit();
            translation.StatusBitNumber = value;
            context.EmitCode(translation.Opcode);
        }
    }
}
