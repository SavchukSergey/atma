using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public class BasePort32BitOpcode : BaseOpcode {

        public BasePort32BitOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public byte Port { get; set; }

        public byte Bit { get; set; }

        protected override void Parse(AsmParser parser) {
            var dest = parser.ReadPort32();
            parser.ReadToken(TokenType.Comma);
            var value = parser.ReadBit();

            Port = dest;
            Bit = value;
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Port32 = Port, BitNumber = Bit };
            output.EmitCode(translation.Opcode);
        }
    }
}
