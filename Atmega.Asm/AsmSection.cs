using System;
using System.Collections.Generic;
using System.Linq;
using Atmega.Asm.Hex;
using Atmega.Hex;

namespace Atmega.Asm {
    public class AsmSection {
        private readonly AsmSectionType _type;

        private readonly IList<byte> _content = new List<byte>();

        public AsmSection(AsmSectionType type) {
            _type = type;
        }

        public int Offset { get; set; }

        public IList<byte> Content {
            get { return _content; }
        }

        public int WordsCount {
            get { return (Content.Count + 1) / 2; }
        }

        public int BytesCount {
            get { return Content.Count; }
        }

        public int VirtualSize {
            get { return BytesCount; }
        }

        public AsmSectionType Type {
            get { return _type; }
        }

        public void EmitCode(ushort opcode) {
            if (_type == AsmSectionType.Data) throw new PureSectionDataException("cannot write initialized data to data section");
            if ((Offset & 0x01) > 0) {
                ReserveByte();
            }
            EmitWord(opcode);
        }

        public void EmitByte(byte bt) {
            if (_type == AsmSectionType.Data) throw new PureSectionDataException("cannot write initialized data to data section");
            _content.Add(bt);
            Offset++;
        }

        public void EmitWord(ushort val) {
            if (_type == AsmSectionType.Data) throw new PureSectionDataException("cannot write initialized data to data section");
            EmitByte((byte)(val & 0xff));
            EmitByte((byte)((val >> 8) & 0xff));
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

        public HexFile BuildHexFile() {
            var hexFile = new HexFile();
            WriteTo(hexFile);
            hexFile.Lines.Add(new HexFileLine { Type = HexFileLineType.Eof });
            return hexFile;
        }

        public void WriteTo(HexFile hexFile) {
            var lines = (Content.Count + 15) / 16;
            for (var i = 0; i < lines; i++) {
                var adr = i * 16;
                var line = new HexFileLine {
                    Address = (ushort)adr,
                    Type = HexFileLineType.Data,
                    Data = Content.Skip(adr).Take(16).ToArray()
                };
                hexFile.Lines.Add(line);
            }
        }

        public void ReserveBytes(int cnt) {
            for (var i = 0; i < cnt; i++) ReserveByte();
        }

        public ushort[] ReadAsUshorts() {
            var res = new ushort[(Content.Count + 1) / 2];
            for (var i = 0; i < Content.Count / 2; i++) {
                var low = Content[i * 2];
                var high = Content[i * 2 + 1];
                res[i] = (ushort)(high * 256 + low);
            }
            if ((Content.Count & 1) != 0) {
                res[res.Length - 1] = Content[Content.Count - 1];
            }
            return res;
        }
    }
}
