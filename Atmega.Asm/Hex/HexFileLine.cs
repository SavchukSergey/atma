using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atmega.Asm.Hex {
    public class HexFileLine {

        public ushort Address { get; set; }

        public byte[] Data { get; set; }

        public HexFileLineType Type { get; set; }

        public static HexFileLine Parse(string line) {
            if (!line.StartsWith(":")) throw new ArgumentException();
            var pointer = 1;
            var cnt = ReadHexByte(ref line, ref pointer);
            var addr = ReadHexUShort(ref line, ref pointer);
            var type = (HexFileLineType)ReadHexByte(ref line, ref pointer);
            var array = new byte[cnt];
            for (var i = 0; i < cnt; i++) {
                array[i] = ReadHexByte(ref line, ref pointer);
            }
            var checksum = ReadHexByte(ref line, ref pointer);

            return new HexFileLine {
                Address = addr,
                Data = array,
                Type = type
            };
        }

        private static ushort ReadHexUShort(ref string line, ref int start) {
            var h = ReadHexByte(ref line, ref start);
            var l = ReadHexByte(ref line, ref start);
            return (ushort)((h << 8) + l);
        }

        private static byte ReadHexByte(ref string line, ref int start) {
            var h = GetHexDigit(line[start++]);
            var l = GetHexDigit(line[start++]);
            return (byte)((h << 4) + l);
        }

        private static byte GetHexDigit(char ch) {
            if (ch >= '0' && ch <= '9') return (byte)(ch - '0');
            if (ch >= 'A' && ch <= 'Z') return (byte)(ch - 'A' + 10);
            if (ch >= 'a' && ch <= 'z') return (byte)(ch - 'a' + 10);
            throw new ArgumentException("Invalid character " + ch);
        }
    }
}
