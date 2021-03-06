﻿namespace Atmega.Asm.Opcodes.Bit.Status {
    public abstract class BaseStatusBitOpcode : BaseOpcode {

        protected BaseStatusBitOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public virtual byte Bit { get; set; }

        protected override void Parse(AsmParser parser) {
            Bit = parser.ReadBit();
        }

        protected override void Compile(AsmSection output) {
            var translation = new OpcodeTranslation {
                Opcode = _opcodeTemplate,
                StatusBitNumber = Bit
            };
            output.EmitCode(translation.Opcode);
        }

        public static BaseStatusBitOpcode FromOpcode(ushort opcode) {
            var clr = (opcode & 0x0080) > 0;
            if (clr) return BaseClearStatusBitOpcode.FromOpcode(opcode);
            return BaseSetStatusBitOpcode.FromOpcode(opcode);
        }
    }
}
