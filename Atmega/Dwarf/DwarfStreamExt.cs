using System;
using System.IO;
using System.Text;
using Atmega.Dwarf.DebugLine;

namespace Atmega.Dwarf {
    public static class DwarfStreamExt {

        public static void WriteDwarf2(this BinaryWriter writer, DebugLineHeader header) {
            writer.Write(header.UnitLength);
            writer.Write(header.Version);
            writer.Write(header.HeaderLength);
            writer.Write(header.MinimumInstructionLength);
            writer.Write(header.DefaultIsStatement);
            writer.Write(header.LineBase);
            writer.Write(header.LineRange);
            writer.Write(header.OpcodeBase);
            if (header.OpcodeBase > 0) {
                if (header.OpcodesArgsCount == null || header.OpcodesArgsCount.Length != header.OpcodeBase - 1) {
                    throw new Exception("Invalid or missing opcode args count");
                }
                foreach (int argsCount in header.OpcodesArgsCount) {
                    writer.Write(argsCount);
                }
            }
            if (header.IncludeDirectories != null) {
                foreach (var dir in header.IncludeDirectories) {
                    writer.WriteDwarfStringZ(dir);
                }
            }
            writer.Write((byte)0);

            if (header.FileNames != null) {
                foreach (var file in header.FileNames) {
                    writer.WriteDwarfStringZ(file.Name);
                    writer.WriteDwarfLeb128(file.DirectoryIndex);
                    writer.WriteDwarfLeb128(file.Time);
                    writer.WriteDwarfLeb128(file.Size);
                }
            }
            writer.Write((byte)0);
        }

        public static void WriteDwarfStringZ(this BinaryWriter writer, string val) {
            var bts = Encoding.UTF8.GetBytes(val);
            writer.Write(bts);
            writer.Write((byte)0);
        }

        public static void WriteDwarfLeb128(this BinaryWriter writer, ulong val) {
            while (val >= 128) {
                var bt = (byte)(0x80 | (val & 0x7f));
                writer.Write(bt);
                val >>= 7;
            }
            writer.Write((byte)val);
        }
    }
}
