namespace Atmega.Asm.Opcodes.Bit.Status {
    public class BsetOpcode : BaseStatusBitOpcode {
        
        public BsetOpcode()
            : base("100101000SSS1000") {
        }

        public override string ToString() {
            switch (Bit) {
                case 0: return "sec";
                case 1: return "sez";
                case 2: return "sen";
                case 3: return "sev";
                case 4: return "ses";
                case 5: return "seh";
                case 6: return "set";
                case 7: return "sei";
                default:
                    return string.Format("bset {0}", Bit);
            }
        }

    }
}
