namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrieOpcode : BaseStatusBitSetBranchOpcode {

        public BrieOpcode()
            : base(7) {
        }

        protected override string OpcodeName { get { return "brie"; } }
    }
}
