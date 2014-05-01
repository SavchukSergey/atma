using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg16Imm8Opcode : BaseOpcode {

        protected BaseReg16Imm8Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.ReadReg16();
            translation.Destination16 = dest;
            context.Queue.Read(TokenType.Comma);
            var value = context.ReadByte();
            translation.Imm8 = value;
            context.EmitCode(translation.Opcode);
        }


    }
}
