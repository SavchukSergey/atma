namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrltOpcode : BaseStatusBitSetBranchOpcode {

        public BrltOpcode()
            : base(4) {
        }

        protected override string OpcodeName { get { return "brlt"; } }
    }
}
