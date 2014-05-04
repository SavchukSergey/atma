using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseImm16Reg32Opcode : BaseOpcode {

        protected BaseImm16Reg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var value = context.Parser.ReadUshort();
            context.Queue.Read(TokenType.Comma);
            var dest = context.Parser.ReadReg32();

            translation.Destination32 = dest;
            context.EmitCode(translation.Opcode);
            context.EmitCode(value);
        }

    }
}
