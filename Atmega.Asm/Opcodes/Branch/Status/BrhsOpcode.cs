namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrhsOpcode : BaseStatusBitSetBranchOpcode {

        public BrhsOpcode()
            : base(5) {
        }

        protected override string OpcodeName { get { return "brhs"; } }

    }
}
