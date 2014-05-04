using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class LdOpcode : BaseOpcode {

        private static readonly ushort _xTemplate = ParseOpcodeTemplate("1001001rrrrr1100");
        private static readonly ushort _yTemplate = ParseOpcodeTemplate("1000001rrrrr1000");
        private static readonly ushort _zTemplate = ParseOpcodeTemplate("1000001rrrrr0000");

        public LdOpcode()
            : base("1000000ddddd1000") {
        }

        public override void Compile(AsmContext context) {
            var dest = context.Parser.ReadReg32();
            context.Queue.Read(TokenType.Comma);
            var operand = context.Parser.ReadIndirectOperand();

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
