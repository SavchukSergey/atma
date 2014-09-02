using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32Port64Opcode : BaseOpcode {

        public byte Port { get; set; }

        public byte Register { get; set; }

        protected BaseReg32Port64Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        protected override void Parse(AsmParser parser) {
            Register = parser.ReadReg32();
            parser.ReadToken(TokenType.Comma);
            Port = parser.ReadPort64();
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation {
                Opcode = _opcodeTemplate,
                Destination32 = Register,
                Port64 = Port
            };
            output.EmitCode(translation.Opcode);
        }

    }
}
