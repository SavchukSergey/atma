namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrneOpcode : BaseStatusBitClearBranchOpcode {

        public BrneOpcode()
            : base(1) {
        }

        protected override string OpcodeName { get { return "brne"; } }
    }
}
