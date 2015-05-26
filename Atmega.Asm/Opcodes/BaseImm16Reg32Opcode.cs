using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseImm16Reg32Opcode : BaseOpcode {

        public byte Source { get; set; }

        public ushort Address { get; set; }

        protected BaseImm16Reg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        protected override void Parse(AsmParser parser) {
            Address = parser.ReadUshort();
            parser.ReadToken(TokenType.Comma);
            Source = parser.ReadReg32();
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Destination32 = Source };
            output.EmitCode(translation.Opcode);
            output.EmitCode(Address);
        }
    }
}
