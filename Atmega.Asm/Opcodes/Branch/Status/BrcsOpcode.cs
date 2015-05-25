namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrcsOpcode : BaseStatusBitSetBranchOpcode {

        public BrcsOpcode()
            : base(0) {
        }

        protected override string OpcodeName { get { return "brcs"; } }
    }
}
