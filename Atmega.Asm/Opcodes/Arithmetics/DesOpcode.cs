namespace Atmega.Asm.Opcodes.Arithmetics {
    public class DesOpcode : BaseOpcode {
        
        public DesOpcode()
            : base("10010100KKKK1011") {
        }

        public override void Compile(AsmContext context) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = context.Parser.ReadDesRound();
            translation.DesRound = dest;
            context.EmitCode(translation.Opcode);
        }
    }
}
