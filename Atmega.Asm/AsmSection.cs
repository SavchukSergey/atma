using System;
using System.Collections.Generic;

namespace Atmega.Asm {
    public class AsmSection {

        private readonly IList<byte> _content = new List<byte>();

        public int Offset { get; set; }

        public IList<byte> Content {
            get { return _content; }
        }

        public void EmitCode(ushort opcode) {
            if ((Offset & 0x01) > 0) {
                ReserveByte();
            }
            EmitByte((byte)(opcode & 0xff));
            EmitByte((byte)((opcode >> 8) & 0xff));
        }

        public void EmitByte(byte bt) {
            _content.Add(bt);
            Offset++;
        }

        public void AlignCode(byte alignment) {
            switch (alignment) {
                case 2:
                    var mask = ~(alignment - 1);
                    var targetOffset = (Offset + 1) & mask;
                    while (Offset < targetOffset) {
                        ReserveByte();
                    }
                    break;
                default:
                    throw new Exception("Invalid alignment");
            }
        }

        public void ReserveByte() {
            //TODO: alignment should take any space if put at the end of section. put uninitialized pointer
            _content.Add(0);
            Offset++;
        }

        public bool TheSame(AsmSection other) {
            if (other == null) return false;
            if (Content.Count != other.Content.Count) return false;
            for (var i = 0; i < Content.Count; i++) {
                if (Content[i] != other.Content[i]) return false;
            }
            return true;
        }
    }
}
