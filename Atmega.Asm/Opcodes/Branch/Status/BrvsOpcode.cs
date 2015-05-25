namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrvsOpcode : BaseStatusBitSetBranchOpcode {

        public BrvsOpcode()
            : base(3) {
        }

        protected override string OpcodeName { get { return "brvs"; } }
    }
}
