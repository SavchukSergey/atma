namespace Atmega.Asm.Opcodes.Bit {
    public class SeiOpcode : BaseSimpleOpcode {

        public SeiOpcode()
            : base("1001010001111000") {
        }

        public override string ToString() {
            return "sei";
        }


    }
}
