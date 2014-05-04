using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class LddOpcode : BaseOpcode {

        private static readonly ushort _yTemplate = ParseOpcodeTemplate("10q0qq0ddddd1qqq");
        private static readonly ushort _zTemplate = ParseOpcodeTemplate("10q0qq0ddddd0qqq");

        public LddOpcode()
            : base("10q0qq0ddddd1qqq") {
        }

        public override void Compile(AsmContext context) {
            var dest = context.Parser.ReadReg32();
            context.Queue.Read(TokenType.Comma);
            var operand = context.Parser.ReadIndirectWithDisplacement();

            var translation = new OpcodeTranslation {
                Opcode = GetOpcodeTemplate(operand.Register),
                Destination32 = dest,
                Displacement = operand.Displacement
            };

            context.EmitCode(translation.Opcode);
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
