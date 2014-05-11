using System;
using System.IO;

namespace Atmega.Asm.Elf {
    public static class ElfStreamExt {

        public static ushort ReadElf32Half(this BinaryReader reader) {
            return reader.ReadUInt16();
        }

        public static void WriteElf32Half(this BinaryWriter writer, ushort val) {
            writer.Write(val);
        }

        public static void WriteElf32Word(this BinaryWriter writer, uint val) {
            writer.Write(val);
        }

        public static void WriteElf32Addr(this BinaryWriter writer, uint val) {
            writer.Write(val);
        }

        public static void WriteElf32Off(this BinaryWriter writer, uint val) {
            writer.Write(val);
        }

        public static void WriteElf32(this BinaryWriter writer, ElfIdentification identification) {
            foreach (var ch in identification.Magic) {
                writer.Write((byte)ch);
            }
            writer.Write((byte)identification.FileClass);
            writer.Write((byte)identification.DataType);
            writer.Write(identification.Version);
            var pad = new byte[9];
            writer.Write(pad);
        }

        public static void WriteElf32(this BinaryWriter writer, ElfHeader header) {
            writer.WriteElf32(header.Identification);
            writer.WriteElf32Half((ushort)header.Type);
            writer.WriteElf32Half(header.Machine);
            writer.WriteElf32Word(header.Version);
            writer.WriteElf32Addr(header.Entry);
            writer.WriteElf32Off(header.ProgramHeaderOffset);
            writer.WriteElf32Off(header.SectionHeaderOffset);
            writer.WriteElf32Word(header.Flags);
            writer.WriteElf32Half(header.ElfHeaderSize);
            writer.WriteElf32Half(header.ProgramHeaderEntrySize);
            writer.WriteElf32Half(header.ProgramHeaderCount);
            writer.WriteElf32Half(header.SectionHeaderEntrySize);
            writer.WriteElf32Half(header.SectionHeaderCount);
            writer.WriteElf32Half(header.StringSectionIndex);
        }
    }
}
