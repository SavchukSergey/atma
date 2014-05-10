using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class LdOpcode : BaseOpcode {

        private static readonly ushort _xTemplate = ParseOpcodeTemplate("1001000rrrrr1100");
        private static readonly ushort _yTemplate = ParseOpcodeTemplate("1000000rrrrr1000");
        private static readonly ushort _yPostPreTemplate = ParseOpcodeTemplate("1001000rrrrr1000");
        private static readonly ushort _zTemplate = ParseOpcodeTemplate("1000000rrrrr0000");
        private static readonly ushort _zPostPreTemplate = ParseOpcodeTemplate("1001000rrrrr0000");

        public LdOpcode()
            : base("1000000ddddd1000") {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var dest = parser.ReadReg32();
            parser.ReadToken(TokenType.Comma);
            var operand = parser.ReadIndirectOperand();

            var translation = new OpcodeTranslation {
                Opcode = GetOpcodeTemplate(operand),
                Destination32 = dest,
                Increment = operand.Increment,
                Decrement = operand.Decrement
            };

            output.EmitCode(translation.Opcode);
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
