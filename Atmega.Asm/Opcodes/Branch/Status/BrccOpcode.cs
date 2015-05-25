namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrccOpcode : BaseStatusBitClearBranchOpcode {

        public BrccOpcode()
            : base(0) {
        }

        protected override string OpcodeName { get { return "brcc"; } }


    }
}
