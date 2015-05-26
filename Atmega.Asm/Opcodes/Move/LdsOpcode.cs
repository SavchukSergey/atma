namespace Atmega.Asm.Opcodes.Move {
    public class LdsOpcode : BaseReg32Imm16Opcode {

        public LdsOpcode()
            : base("1001000ddddd0000") {
        }

        public override string ToString() {
            return string.Format("lds {0}, {1}", FormatRegister(Destination), FormatAddress(Address));
        }
    }
}
