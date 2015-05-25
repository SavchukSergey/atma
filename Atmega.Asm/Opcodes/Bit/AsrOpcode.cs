namespace Atmega.Asm.Opcodes.Bit {
    public class AsrOpcode: BaseReg32Opcode {

        public AsrOpcode()
            : base("1001010rrrrr0101") {
        }

        public override string ToString() {
            return string.Format("asr {0}", FormatRegister(Register));
        }
    }
}
