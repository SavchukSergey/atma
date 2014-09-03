namespace Atmega.Asm.Opcodes.Arithmetics {
    public class SbiwOpcode : BaseRegWImm6Opcode {

        public SbiwOpcode()
            : base("10010111KKddKKKK") {
        }

        public override string ToString() {
            return string.Format("sbiw {0}, {1}", FormatRegister(Register), Value);
        }
    }
}
