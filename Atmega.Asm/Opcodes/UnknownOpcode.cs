using System;

namespace Atmega.Asm.Opcodes {
    public class UnknownOpcode : BaseOpcode {
        
        public UnknownOpcode()
            : base("0000000000000000") {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            throw new NotImplementedException();
        }

        public ushort Opcode { get; set; }
    }
}
