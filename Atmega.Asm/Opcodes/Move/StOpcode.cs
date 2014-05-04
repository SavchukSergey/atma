using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class StOpcode : BaseOpcode {

        private static readonly ushort _xTemplate = ParseOpcodeTemplate("1001000ddddd1100");
        private static readonly ushort _yTemplate = ParseOpcodeTemplate("1000000ddddd1000");
        private static readonly ushort _zTemplate = ParseOpcodeTemplate("1000000ddddd0000");

        public StOpcode()
            : base("1000000ddddd1000") {
        }

        public override void Compile(AsmContext context) {
            var operand = context.ReadIndirectOperand();
            context.Queue.Read(TokenType.Comma);
            var dest = context.ReadReg32();

            var translation = new OpcodeTranslation {
                Opcode = GetOpcodeTemplate(operand.Register),
                Destination32 = dest,
                Increment = operand.Increment,
                Decrement = operand.Decrement
            };

            context.EmitCode(translation.Opcode);
        }

        private static ushort GetOpcodeTemplate(IndirectRegister register) {
            switch (register) {
                case IndirectRegister.X: return _xTemplate;
                case IndirectRegister.Y: return _yTemplate;
                case IndirectRegister.Z: return _zTemplate;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
