namespace Atmega.Asm.Opcodes.Arithmetics {
    public class DesOpcode : BaseOpcode {
        
        public DesOpcode()
            : base("10010100KKKK1011") {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate };
            var dest = parser.ReadDesRound();
            translation.DesRound = dest;
            output.EmitCode(translation.Opcode);
        }
    }
}
