using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg16ComplementImm8Opcode : BaseOpcode {

        protected BaseReg16ComplementImm8Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.Parser.ReadReg16();
            translation.Destination16 = dest;
            context.Queue.Read(TokenType.Comma);
            var value = context.Parser.ReadByte();
            translation.Imm8 = (byte)(255 - value);
            context.EmitCode(translation.Opcode);
        }


    }
}
