namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BridOpcode : BaseStatusBitClearBranchOpcode {

        public BridOpcode()
            : base(7) {
        }

        protected override string OpcodeName { get { return "brid"; } }

    }
}
