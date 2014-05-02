namespace Atmega.Asm.Opcodes {
    public struct OpcodeTranslation {

        public ushort Opcode { get; set; }

        /*
        public IndirectRegister YZSelector {
            get { return (Opcode & 0x08) > 0 ? IndirectRegister.Y : IndirectRegister.Z; }
        }

        public byte YZOffset {
            get { return (byte)(((Opcode & 0x2000) >> 8) | ((Opcode & 0x0c00) >> 7) | (Opcode & 0x0f)); }
        }

        public byte UImm6 {
            get { return (byte)(((Opcode & 0xc0) >> 2) | (Opcode & 0x0f)); }
        }

        public bool Increment {
            get { return (Opcode & 0x01) > 0; }
        }

        public bool Decrement {
            get { return (Opcode & 0x02) > 0; }
        }

        public IndirectRegister IndirectWordRegister {
            get {
                var raw = (Opcode >> 4) & 0x3;
                switch (raw) {
                    case 0:
                        return IndirectRegister.R24;
                    case 1:
                        return IndirectRegister.X;
                    case 2:
                        return IndirectRegister.Y;
                    case 3:
                        return IndirectRegister.Z;
                }
                throw new InvalidOperationException();
            }
        }

        public byte DestinationWordRegister {
            get { return (byte)((Opcode >> 3) & 0x1e); }
        }

        public byte SourceWordRegister {
            get { return (byte)((Opcode << 1) & 0x1e); }
        }


        public short Offset12 {
            get {
                var offset = Opcode & 0xfff;
                if (offset >= 0x800) offset = -(((offset ^ 0xfff) + 1) & 0xfff);
                return (short)offset;
            }
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

        public byte Destination16 {
            get { return (byte)(16 + (byte)((Opcode & 0x00f0) >> 4)); }
            set {
                Opcode &= 0xff0f;
                value -= 16;
                Opcode |= (ushort)((value & 0x0f) << 4);
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
    }
}
