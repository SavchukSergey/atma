using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BasePort64Reg32Opcode : BaseOpcode {

        public byte Port { get; set; }

        public byte Register { get; set; }

        protected BasePort64Reg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        protected override void Parse(AsmParser parser) {
            Port = parser.ReadPort64();
            parser.ReadToken(TokenType.Comma);
            Register = parser.ReadReg32();
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation {
                Opcode = _opcodeTemplate,
                Port64 = Port,
                Destination32 = Register
            };
            output.EmitCode(translation.Opcode);
        }
    }
}
