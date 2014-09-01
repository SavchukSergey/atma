using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32Reg32Opcode : BaseOpcode {

        public byte Register { get; set; }

        public byte Destination { get; set; }


        protected BaseReg32Reg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }


        protected override void Parse(AsmParser parser) {
            Destination = parser.ReadReg32();
            parser.ReadToken(TokenType.Comma);
            Register = parser.ReadReg32();
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Destination32 = Destination, Register32 = Register };
            output.EmitCode(translation.Opcode);
        }
    }
}
