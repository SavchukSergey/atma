using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public class BaseReg32BitOpcode : BaseOpcode {
        public BaseReg32BitOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.ReadReg32();
            translation.Destination32 = dest;
            context.Queue.Read(TokenType.Comma);
            var value = context.ReadBit();
            translation.BitNumber = value;
            context.EmitCode(translation.Opcode);
        }
    }
}
