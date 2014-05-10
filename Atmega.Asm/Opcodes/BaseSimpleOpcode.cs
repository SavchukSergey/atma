namespace Atmega.Asm.Opcodes {
    public class BaseSimpleOpcode : BaseOpcode {

        public BaseSimpleOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            output.EmitCode(_opcodeTemplate);
        }
    }
}
