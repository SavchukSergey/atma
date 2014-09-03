using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg16Reg16Opcode : BaseOpcode {

        public byte Register { get; set; }

        public byte Destination { get; set; }

        protected BaseReg16Reg16Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        protected override void Parse(AsmParser parser) {
            Destination = parser.ReadReg16();
            parser.ReadToken(TokenType.Comma);
            Register = parser.ReadReg16();

        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Destination16 = Destination, Register16 = Register };
            output.EmitCode(translation.Opcode);
        }
    }
}
