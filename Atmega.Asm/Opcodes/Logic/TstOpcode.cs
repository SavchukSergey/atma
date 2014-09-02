namespace Atmega.Asm.Opcodes.Logic {
    //todo: subclass of AndOpcode
    public class TstOpcode: BaseReg32SelfOpcode {

        public TstOpcode()
            : base("001000rdddddrrrr") {
        }
     
    }
}
