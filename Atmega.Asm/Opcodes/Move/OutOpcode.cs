namespace Atmega.Asm.Opcodes.Move {
    public class OutOpcode : BasePort64Reg32Opcode {

        public OutOpcode()
            : base("10111PPrrrrrPPPP") {
        }

        public override string ToString() {
            return string.Format("out {0}, {1}", FormatPort(Port), FormatRegister(Register));
        }
    }
}
