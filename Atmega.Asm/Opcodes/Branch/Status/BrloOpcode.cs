﻿namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BrloOpcode : BaseStatusBitSetBranchOpcode {

        public BrloOpcode()
            : base(0) {
        }

        protected override string OpcodeName { get { return "brlo"; } }
    }
}
