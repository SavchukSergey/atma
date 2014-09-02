namespace Atmega.Asm.Opcodes.Move {
    public class InOpcode : BaseReg32Port64Opcode {
        
        public InOpcode()
            : base("10110PPdddddPPPP") {
        }

        public override string ToString() {
            return string.Format("in {0}, {1}", FormatRegister(Register), FormatPort(Port));
        }
    }
}
