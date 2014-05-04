using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Move {
    public class LdOpcode : BaseOpcode {

        private static ushort _xTemplate = ParseOpcodeTemplate("1001000ddddd1100");
        private static ushort _yTemplate = ParseOpcodeTemplate("1000000ddddd1000");
        private static ushort _zTemplate = ParseOpcodeTemplate("1000000ddddd0000");

        public LdOpcode()
            : base("1000000ddddd1000") {
        }

        public override void Compile(AsmContext context) {
            var dest = context.ReadReg32();

            context.Queue.Read(TokenType.Comma);

            var decrement = false;
            if (context.Queue.Peek().Type == TokenType.Minus) {
                decrement = true;
                context.Queue.Read(TokenType.Minus);
            }

            var reg = context.ReadIndirectReg();

            var increment = false;
            if (context.Queue.Peek().Type == TokenType.Plus) {
                increment = true;
                context.Queue.Read(TokenType.Plus);
            }

            if (increment && decrement) {
                throw new TokenException("Only pre-decrement or post-increment can be specified at one time", context.Queue.LastReadToken);
            }

            var translation = new OpcodeTranslation { Opcode = GetOpcodeTemplate(reg) };
            translation.Destination32 = dest;
            translation.Increment = increment;
            translation.Decrement = decrement;

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
