namespace Atmega.Asm.Opcodes.Move {
    public class PushOpcode : BaseMultiReg32Opcode {

        public PushOpcode()
            : base("1001001rrrrr1111") {
        }

        public override string ToString() {
            return string.Format("push {0}", FormatRegisters(Registers));
        }

    }
}
