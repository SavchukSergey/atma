using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public class BasePort32BitOpcode : BaseOpcode {
        public BasePort32BitOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.Parser.ReadPort32();
            translation.Port32 = dest;
            context.Queue.Read(TokenType.Comma);
            var value = context.Parser.ReadBit();
            translation.BitNumber = value;
            context.EmitCode(translation.Opcode);
        }
    }
}
