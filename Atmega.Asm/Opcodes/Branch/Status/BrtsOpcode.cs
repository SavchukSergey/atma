namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrtsOpcode : BaseStatusBitSetBranchOpcode {

        public BrtsOpcode()
            : base(6) {
        }

        protected override string OpcodeName { get { return "brts"; } }

    }
}
