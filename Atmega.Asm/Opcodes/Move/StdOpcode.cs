using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class StdOpcode : BaseOpcode {

        private static readonly ushort _yTemplate = ParseOpcodeTemplate("10q0qq1rrrrr1qqq");
        private static readonly ushort _zTemplate = ParseOpcodeTemplate("10q0qq1rrrrr0qqq");

        public StdOpcode()
            : base("10q0qq1rrrrr1qqq") {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var operand = parser.ReadIndirectWithDisplacement();
            parser.ReadToken(TokenType.Comma);
            var dest = parser.ReadReg32();

            var translation = new OpcodeTranslation {
                Opcode = GetOpcodeTemplate(operand.Register),
                Destination32 = dest,
                Displacement = operand.Displacement
            };

            output.EmitCode(translation.Opcode);
        }

        private static ushort GetOpcodeTemplate(IndirectRegister register) {
            switch (register) {
                case IndirectRegister.Y: return _yTemplate;
                case IndirectRegister.Z: return _zTemplate;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
