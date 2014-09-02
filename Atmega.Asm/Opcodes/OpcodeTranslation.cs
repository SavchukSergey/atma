namespace Atmega.Asm.Opcodes {
    public struct OpcodeTranslation {

        public ushort Opcode { get; set; }

        /*

        public byte YZOffset {
            get { return (byte)(((Opcode & 0x2000) >> 8) | ((Opcode & 0x0c00) >> 7) | (Opcode & 0x0f)); }
        }

        public byte DestinationWordRegister {
            get { return (byte)((Opcode >> 3) & 0x1e); }
        }

        public byte SourceWordRegister {
            get { return (byte)((Opcode << 1) & 0x1e); }
        }

        */

        public byte Destination32 {
            get { return (byte)((Opcode & 0x01f0) >> 4); }
            set {
                Opcode &= 0xfe0f;
                Opcode |= (ushort)((value & 0x1f) << 4);
            }
        }

        public byte Register32 {
            get {
                return (byte)((Opcode & 0x0f) + ((Opcode & 0x0200) >> 5));
            }
            set {
                Opcode &= 0xfdf0;
                Opcode |= (ushort)(value & 0x0f);
                Opcode |= (ushort)((value & 0x10) << 5);
            }
        }

        public byte Register16 {
            get {
                return (byte)(16 + (Opcode & 0x0f));
            }
            set {
                var val = value - 16;
                Opcode &= 0xfff0;
                Opcode |= (ushort)(val & 0x0f);
            }
        }

        public byte Destination16 {
            get { return (byte)(16 + (byte)((Opcode & 0x00f0) >> 4)); }
            set {
                Opcode &= 0xff0f;
                value -= 16;
                Opcode |= (ushort)((value & 0x0f) << 4);
            }
        }

        public byte Register16Word {
            get {
                return (byte)(2 * (Opcode & 0x0f));
            }
            set {
                var val = value / 2;
                Opcode &= 0xfff0;
                Opcode |= (ushort)(val & 0x0f);
            }
        }

        public byte Destination16Word {
            get { return (byte)(2 * (byte)((Opcode & 0x00f0) >> 4)); }
            set {
                Opcode &= 0xff0f;
                value /= 2;
                Opcode |= (ushort)((value & 0x0f) << 4);
            }
        }

        public byte Register8 {
            get {
                return (byte)(16 + (Opcode & 0x07));
            }
            set {
                var val = value - 16;
                Opcode &= 0xfff8;
                Opcode |= (ushort)(val & 0x07);
            }
        }

        public byte Destination8 {
            get { return (byte)(16 + (byte)((Opcode & 0x0070) >> 4)); }
            set {
                Opcode &= 0xff8f;
                value -= 16;
                Opcode |= (ushort)((value & 0x07) << 4);
            }
        }

        public byte Imm8 {
            get {
                return (byte)(((Opcode & 0x0f00) >> 4) | (Opcode & 0x0f));
            }
            set {
                Opcode &= 0xf0f0;
                Opcode |= (ushort)(value & 0x0f);
                Opcode |= (ushort)((value << 4) & 0x0f00);
            }
        }

        public byte Imm6 {
            get {
                return (byte)(((Opcode & 0x00c0) >> 2) | (Opcode & 0x0f));
            }
            set {
                Opcode &= 0xff30;
                Opcode |= (ushort)(value & 0x0f);
                Opcode |= (ushort)((value << 2) & 0x00c0);
            }
        }

        public byte Port32 {
            get {
                return (byte)((Opcode >> 3) & 0x1f);
            }
            set {
                Opcode &= 0xff07;
                Opcode |= (ushort)((value & 0x1f) << 3);
            }
        }

        public byte Port64 {
            get { return (byte)(((Opcode & 0x0600) >> 5) | (Opcode & 0x0f)); }
            set {
                Opcode &= 0xf9f0;
                Opcode |= (ushort)(value & 0x0f);
                Opcode |= (ushort)((value << 5) & 0x0600);
            }
        }

        public byte BitNumber {
            get { return (byte)(Opcode & 0x7); }
            set {
                Opcode &= 0xfff8;
                Opcode |= (ushort)(value & 0x07);
            }
        }

        public byte StatusBitNumber {
            get { return (byte)((Opcode & 0x0070) >> 4); }
            set {
                Opcode &= 0xff8f;
                Opcode |= (ushort)((value << 4) & 0x0070);
            }
        }

        public sbyte Offset7 {
            get {
                var offset = (Opcode >> 3) & 0x7f;
                if (offset >= 0x40) offset = -(((offset ^ 0x7f) + 1) & 0x7f);
                return (sbyte)offset;
            }
            set {
                Opcode &= 0xfc07;
                if (value < 0) {
                    value = (sbyte)(256 + value);
                }
                Opcode |= (ushort)((value << 3) & 0x03f8);
            }
        }

        public short Offset12 {
            get {
                var offset = (short)(Opcode & 0x0fff);
                if (offset >= 0x800) {
                    return (short)(offset - 4096);
                }
                return offset;
            }
            set {
                Opcode &= 0xf000;
                if (value < 0) {
                    value = (short)(4096 + value);
                }
                Opcode |= (ushort)(value & 0xfff);
            }
        }

        public byte Offset22High {
            get {
                var offset = 0;
                offset |= (Opcode & 0x01f0) >> 3;
                offset |= (Opcode & 0x0001);
                return (byte)offset;
            }
            set {
                Opcode &= 0xfe0e;
                Opcode |= (ushort)((value << 3) & 0x01f0);
                Opcode |= (ushort)(value & 0x0001);
            }
        }

        public byte RegW24 {
            get {
                var val = (Opcode & 0x0030) >> 4;
                val = val * 2 + 24;
                return (byte)val;
            }
            set {
                Opcode &= 0xffcf;
                var val = (value - 24) / 2;
                Opcode |= (ushort)((val << 4) & 0x0030);
            }
        }

        public bool Increment {
            get { return (Opcode & 0x01) > 0; }
            set {
                Opcode &= 0xfffe;
                if (value) Opcode |= 1;
            }
        }

        public bool Decrement {
            get { return (Opcode & 0x02) > 0; }
            set {
                Opcode &= 0xfffd;
                if (value) Opcode |= 2;
            }
        }

        public bool SpmIncrement {
            get { return (Opcode & 0x10) > 0; }
            set {
                Opcode &= 0xffef;
                if (value) Opcode |= 0x10;
            }
        }

        public byte Displacement {
            get {
                var offset = 0;
                offset |= (Opcode & 0x2000) >> 8;
                offset |= (Opcode & 0x0c00) >> 7;
                offset |= (Opcode & 0x0007) >> 0;
                return (byte)offset;
            }
            set {
                Opcode &= 0xd3f8;
                Opcode |= (ushort)((value << 0) & 0x0007);
                Opcode |= (ushort)((value << 7) & 0x0c00);
                Opcode |= (ushort)((value << 8) & 0x2000);
            }
        }

        public byte DesRound {
            get {
                return (byte)((Opcode & 0x00f0) >> 4);
            }
            set {
                Opcode &= 0xff0f;
                Opcode |= (ushort)((value << 4) & 0x00f0);
            }
        }
    }

    public enum IndirectRegister {
        X,
        Y,
        Z
    }

    public struct IndirectOperand {

        public IndirectRegister Register;

        public bool Increment;

        public bool Decrement;

    }

    public struct IndirectOperandWithDisplacement {

        public IndirectRegister Register;

        public byte Displacement;

    }
}
