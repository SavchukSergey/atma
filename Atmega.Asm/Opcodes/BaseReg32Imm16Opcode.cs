using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg32Imm16Opcode : BaseOpcode {

        protected BaseReg32Imm16Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.Parser.ReadReg32();
            translation.Destination32 = dest;
            context.Queue.Read(TokenType.Comma);
            context.EmitCode(translation.Opcode);
            var value = context.Parser.ReadUshort();
            context.EmitCode(value);
        }

    }
}
