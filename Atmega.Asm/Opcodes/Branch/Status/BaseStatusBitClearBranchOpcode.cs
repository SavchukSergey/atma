namespace Atmega.Asm.Opcodes.Branch.Status {
    public abstract class BaseStatusBitClearBranchOpcode : BrbcOpcode {

        private readonly byte _bit;

        protected BaseStatusBitClearBranchOpcode(byte bit) {
            _bit = bit;
        }

        public override byte Bit { get { return _bit; } set { } }

        protected override void Parse(AsmParser parser) {
            ParseOffset(parser);
        }

        public static new BrbcOpcode FromOpcode(ushort opcode) {
            var translation = new OpcodeTranslation { Opcode = opcode };
            var bit = translation.BitNumber;
            var delta = (short)(translation.Offset7 * 2);
            switch (bit) {
                case 0: return new BrccOpcode { Delta = delta};
                case 1: return new BrneOpcode { Delta = delta};
                case 2: return new BrplOpcode { Delta = delta};
                case 3: return new BrvcOpcode { Delta = delta};
                case 4: return new BrgeOpcode { Delta = delta};
                case 5: return new BrhcOpcode { Delta = delta};
                case 6: return new BrtcOpcode { Delta = delta};
                case 7: return new BridOpcode { Delta = delta};
                default:
                    return new BrbcOpcode { Bit = bit, Delta = delta };
            }
        }
    }
}
