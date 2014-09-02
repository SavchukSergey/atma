using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public class BaseReg32BitOpcode : BaseOpcode {

        public byte Register { get; set; }

        public byte Bit { get; set; }

        public BaseReg32BitOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        protected override void Parse(AsmParser parser) {
            Register = parser.ReadReg32();
            parser.ReadToken(TokenType.Comma);
            Bit = parser.ReadBit();
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation {
                Opcode = _opcodeTemplate,
                Destination32 = Register,
                BitNumber = Bit
            };
            output.EmitCode(translation.Opcode);
        }
    }
}
