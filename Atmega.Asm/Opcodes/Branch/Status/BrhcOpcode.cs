namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrhcOpcode : BaseStatusBitClearBranchOpcode {

        public BrhcOpcode()
            : base(5) {
        }

        protected override string OpcodeName { get { return "brhc"; } }
    }
}
