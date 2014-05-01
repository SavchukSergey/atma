namespace Atmega.Asm.Opcodes {
    public abstract class BaseOpcode {

        protected readonly ushort _opcodeTemplate;

        protected BaseOpcode(string opcodeTemplate) {
            ushort val = 0;
            for (var i = 0; i < 16; i++) {
                var bit = opcodeTemplate[i];
                if (bit == '1') {
                    val = (ushort)(val * 2 + 1);
                } else {
                    val = (ushort)(val * 2);
                }
            }
            _opcodeTemplate = val;
        }

        public abstract void Compile(AsmContext context);

    }
}
