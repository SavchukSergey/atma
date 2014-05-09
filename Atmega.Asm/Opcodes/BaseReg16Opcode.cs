namespace Atmega.Asm.Opcodes {
    public abstract class BaseReg16Opcode : BaseOpcode {

        protected BaseReg16Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.Parser.ReadReg16();
            translation.Destination16 = dest;
            context.EmitCode(translation.Opcode);
        }

    }
}
