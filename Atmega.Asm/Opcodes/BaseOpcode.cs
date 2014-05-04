namespace Atmega.Asm.Opcodes {
    public abstract class BaseOpcode {

        protected readonly ushort _opcodeTemplate;

        protected BaseOpcode(string opcodeTemplate) {
            _opcodeTemplate = ParseOpcodeTemplate(opcodeTemplate);
        }

        public abstract void Compile(AsmContext context);

        protected static ushort ParseOpcodeTemplate(string template) {
            ushort val = 0;
            for (var i = 0; i < 16; i++) {
                var bit = template[i];
                if (bit == '1') {
                    val = (ushort)(val * 2 + 1);
                } else {
                    val = (ushort)(val * 2);
                }
            }
            return val;
        }
    }
}
