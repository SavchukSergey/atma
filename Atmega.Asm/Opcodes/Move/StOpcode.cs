using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class StOpcode : BaseOpcode {

        private static readonly ushort _xTemplate = ParseOpcodeTemplate("1001001ddddd1100");
        private static readonly ushort _yTemplate = ParseOpcodeTemplate("1000001ddddd1000");
        private static readonly ushort _zTemplate = ParseOpcodeTemplate("1000001ddddd0000");
        private static readonly ushort _yPostPreTemplate = ParseOpcodeTemplate("1001001rrrrr1000");
        private static readonly ushort _zPostPreTemplate = ParseOpcodeTemplate("1001001rrrrr0000");

        public StOpcode()
            : base("1001001ddddd1100") {
        }

        public IndirectOperand Operand { get; set; }

        public byte Source { get; set; }

        protected override void Parse(AsmParser parser) {
            Operand = parser.ReadIndirectOperand();
            parser.ReadToken(TokenType.Comma);
            Source = parser.ReadReg32();
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation {
                Opcode = GetOpcodeTemplate(Operand),
                Destination32 = Source,
                Increment = Operand.Increment,
                Decrement = Operand.Decrement
            };

            output.EmitCode(translation.Opcode);
        }

        public override string ToString() {
            return string.Format("st {0}, {1}", Operand, FormatRegister(Source));
        }

        private static ushort GetOpcodeTemplate(IndirectOperand operand) {
            switch (operand.Register) {
                case IndirectRegister.X: return _xTemplate;
                case IndirectRegister.Y:
                    if (operand.Increment || operand.Decrement) return _yPostPreTemplate;
                    return _yTemplate;
                case IndirectRegister.Z:
                    if (operand.Increment || operand.Decrement) return _zPostPreTemplate;
                    return _zTemplate;
                default:
                    throw new InvalidOperationException();
            }
        }

    }
}
