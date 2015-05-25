namespace Atmega.Asm.Opcodes.Branch {
    public class JmpOpcode : BaseOffset22Opcode {

        public JmpOpcode()
            : base("1001010hhhhh110h") {
        }

        public override string ToString() {
            return string.Format("jmp {0}", FormatBranchTarget(Target));
        }
    }
}
