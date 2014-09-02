namespace Atmega.Asm.Opcodes.Move {
    public class PopOpcode : BaseMultiReg32Opcode {

        public PopOpcode()
            : base("1001000rrrrr1111") {
        }

        public override string ToString() {
            return string.Format("pop {0}", FormatRegisters(Registers));
        }
     
    }
}
