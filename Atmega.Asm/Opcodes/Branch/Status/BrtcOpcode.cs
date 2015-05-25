namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrtcOpcode : BaseStatusBitClearBranchOpcode {

        public BrtcOpcode()
            : base(6) {
        }

        protected override string OpcodeName { get { return "brtc"; } }

    }
}
