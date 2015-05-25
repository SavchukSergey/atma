namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrshOpcode : BaseStatusBitClearBranchOpcode {

        public BrshOpcode()
            : base(0) {
        }

        protected override string OpcodeName { get { return "brsh"; } }
    }
}
