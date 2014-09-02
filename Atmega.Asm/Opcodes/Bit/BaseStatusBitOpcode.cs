﻿namespace Atmega.Asm.Opcodes.Bit {
    public abstract class BaseStatusBitOpcode : BaseOpcode {

        protected BaseStatusBitOpcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public byte Bit { get; set; }

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
    }
}
