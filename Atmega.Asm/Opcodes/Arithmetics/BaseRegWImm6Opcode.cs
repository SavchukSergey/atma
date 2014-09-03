using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Arithmetics {
    public class BaseRegWImm6Opcode : BaseOpcode {
        
        public BaseRegWImm6Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public byte Register { get; set; }

        public byte Value { get; set; }

        protected override void Parse(AsmParser parser) {
            Register = parser.ReadRegW24();
            parser.ReadToken(TokenType.Comma);
            Token exprToken;
            var imm = parser.CalculateExpression(out exprToken);
            if (imm > 63) {
                throw new TokenException("value must be less than 64", exprToken);
            }
            Value = (byte)imm;
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, RegW24 = Register, Imm6 = Value };
            output.EmitCode(translation.Opcode);
        }

    }
}
