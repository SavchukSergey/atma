namespace Atmega.Asm.Opcodes {
    public class BaseSimpleOpcode : BaseOpcode {

        public BaseSimpleOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmContext context) {
            context.EmitCode(_opcodeTemplate);
        }
    }
}
