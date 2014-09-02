namespace Atmega.Asm.Opcodes.Bit.Status {
    public class BclrOpcode : BaseStatusBitOpcode {
        
        public BclrOpcode()
            : base("100101001SSS1000") {
        }

        public override string ToString() {
            switch (Bit) {
                case 0: return "clc";
                case 1: return "clz";
                case 2: return "cln";
                case 3: return "clv";
                case 4: return "cls";
                case 5: return "clh";
                case 6: return "clt";
                case 7: return "cli";
                default:
                    return string.Format("bclr {0}", Bit);
            }
        }
    }
}
