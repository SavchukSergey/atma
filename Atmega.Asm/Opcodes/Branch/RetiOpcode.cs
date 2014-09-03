﻿namespace Atmega.Asm.Opcodes.Branch {
    public class RetiOpcode : BaseSimpleOpcode {

        public RetiOpcode()
            : base("1001010100011000") {
        }

        public override string ToString() {
            return "reti";
        }

    }
}
