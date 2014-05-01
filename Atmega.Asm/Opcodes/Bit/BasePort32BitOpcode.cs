using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Bit {
    public class BasePort32BitOpcode : BaseOpcode {
        public BasePort32BitOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.ReadPort32();
            translation.Port32 = dest;
            context.Queue.Read(TokenType.Comma);
            var value = context.ReadBit();
            translation.BitNumber = value;
            context.EmitCode(translation.Opcode);
        }
    }
}
