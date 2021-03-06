﻿namespace Atmega.Asm.Opcodes.Branch {
    public class RjmpOpcode : BaseOffset12Opcode {

        public RjmpOpcode()
            : base("1100LLLLLLLLLLLL") {
        }

        public override string ToString() {
            return string.Format("rjmp {0}", FormatOffset(Delta, 1));
        }

    }
}
