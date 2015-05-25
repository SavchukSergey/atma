namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrgeOpcode : BaseStatusBitClearBranchOpcode {

        public BrgeOpcode()
            : base(4) {
        }

        protected override string OpcodeName { get { return "brge"; } }

    }
}
