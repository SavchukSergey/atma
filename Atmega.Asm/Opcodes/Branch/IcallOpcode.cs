﻿namespace Atmega.Asm.Opcodes.Branch {
    public class IcallOpcode : BaseSimpleOpcode {

        public IcallOpcode()
            : base("1001010100001001") {
        }

        public override string ToString() {
            return "icall";
        }
    }
}
