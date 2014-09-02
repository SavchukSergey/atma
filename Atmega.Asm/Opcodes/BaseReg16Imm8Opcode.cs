using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg16Imm8Opcode : BaseOpcode {

        public byte Register { get; set; }

        public byte Value { get; set; }


        protected BaseReg16Imm8Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        protected override void Parse(AsmParser parser) {
            Register = parser.ReadReg16();
            parser.ReadToken(TokenType.Comma);
            Value = parser.ReadByte();
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation {
                Opcode = _opcodeTemplate,
                Destination16 = Register,
                Imm8 = Value
            };
            output.EmitCode(translation.Opcode);
        }
    }
}
