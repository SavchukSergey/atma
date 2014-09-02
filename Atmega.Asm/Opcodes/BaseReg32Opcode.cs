namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32Opcode : BaseOpcode {

        public byte Register { get; set; }

        protected BaseReg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        protected override void Parse(AsmParser parser) {
            Register = parser.ReadReg32();
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation {
                Opcode = _opcodeTemplate,
                Destination32 = Register
            };
            output.EmitCode(translation.Opcode);
        }
    }
}
