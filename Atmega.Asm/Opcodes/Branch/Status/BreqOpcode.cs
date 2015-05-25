namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BreqOpcode : BaseStatusBitSetBranchOpcode {
        
        public BreqOpcode()
            : base(1) {
        }

        protected override string OpcodeName { get { return "breq"; } }
    }
}
