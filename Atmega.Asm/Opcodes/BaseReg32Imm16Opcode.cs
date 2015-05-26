using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32Imm16Opcode : BaseOpcode {

        public byte Destination { get; set; }

        public ushort Address { get; set; }

        protected BaseReg32Imm16Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        protected override void Parse(AsmParser parser) {
            Destination = parser.ReadReg32();
            parser.ReadToken(TokenType.Comma);
            Address = parser.ReadUshort();
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Destination32 = Destination };
            output.EmitCode(translation.Opcode);
            output.EmitCode(Address);
        }
    }
}
