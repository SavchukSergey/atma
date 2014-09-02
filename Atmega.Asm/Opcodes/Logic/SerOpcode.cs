using Atmega.Asm.Opcodes.Move;

namespace Atmega.Asm.Opcodes.Logic {
    public class SerOpcode : LdiOpcode {

        public override byte Value {
            get { return 255; }
            set { }
        }

        protected override void Parse(AsmParser parser) {
            Register = parser.ReadReg16();
        }
    }
}
