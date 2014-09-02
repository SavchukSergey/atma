namespace Atmega.Asm.Opcodes {
    public class BaseSimpleOpcode : BaseOpcode {

        public BaseSimpleOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        protected override void Compile(AsmSection output) {
            output.EmitCode(_opcodeTemplate);
        }

    }
}
