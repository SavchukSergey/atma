namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrplOpcode : BaseStatusBitClearBranchOpcode {

        public BrplOpcode()
            : base(2) {
        }

        protected override string OpcodeName { get { return "brpl"; } }

    }
}
