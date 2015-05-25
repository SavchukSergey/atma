namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrmiOpcode : BaseStatusBitSetBranchOpcode {

        public BrmiOpcode()
            : base(2) {
        }

        protected override string OpcodeName { get { return "brmi"; } }
    }
}
