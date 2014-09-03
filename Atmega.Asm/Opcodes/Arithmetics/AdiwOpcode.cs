namespace Atmega.Asm.Opcodes.Arithmetics {
    public class AdiwOpcode : BaseRegWImm6Opcode {

        public AdiwOpcode()
            : base("10010110KKddKKKK") {
        }

        public override string ToString() {
            return string.Format("adiw {0}, {1}", FormatRegister(Register), Value);
        }
    }
}
