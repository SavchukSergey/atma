namespace Atmega.Asm.Opcodes.Branch {
    public class CallOpcode : BaseOffset22Opcode {

        public CallOpcode()
            : base("1001010hhhhh111h") {
        }

        public override string ToString() {
            return string.Format("call {0}", FormatBranchTarget(Target));
        }

    }
}
