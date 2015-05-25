namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrvcOpcode : BaseStatusBitClearBranchOpcode {

        public BrvcOpcode()
            : base(3) {
        }

        protected override string OpcodeName { get { return "brvc"; } }
    }
}
