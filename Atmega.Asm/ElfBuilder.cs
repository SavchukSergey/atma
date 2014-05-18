using System.Diagnostics;
using System.IO;
using System.Linq;
using Atmega.Elf;

namespace Atmega.Asm {
    public class ElfBuilder {

        public void Save(AsmContext context, Stream stream) {
            var file = new ElfFile();
            var writer = new BinaryWriter(stream);
            file.Sections.Add(new ElfSection());
            AddCode(file, context);
            AddData(file, context);
            AddFlash(file, context);

            var sectionsStringsIndex = file.AddStringsSection();
            
            const int headerSize = 0x34;
            const int segmentsOffset = headerSize;
            const int segmentEntrySize = 0x20;
            var dataOffset = (uint) (segmentsOffset + file.Segments.Count * segmentEntrySize);
            var sectionsOffset = dataOffset + file.Data.Length;
            var header = new ElfHeader {
                Identification = {
                    Magic = new[] { (char)0x7f, 'E', 'L', 'F' },
                    FileClass = ElfFileClass.Elf32,
                    DataType = ElfDataType.Lsb,
                    Version = 1,
                },
                Type = ElfType.Executable,
                Machine = 0x53,
                Version = 1,
                Entry = 0x0,
                ProgramHeaderOffset = segmentsOffset,
                SectionHeaderOffset = (uint)sectionsOffset,
                Flags = 0x84,
                ElfHeaderSize = headerSize,
                ProgramHeaderEntrySize = segmentEntrySize,
                ProgramHeaderCount = (ushort)file.Segments.Count,
                SectionHeaderEntrySize = 0x28,
                SectionHeaderCount = (ushort)file.Sections.Count,
                StringSectionIndex = (ushort)sectionsStringsIndex
            };
            writer.WriteElf32(header);
            foreach (var segment in file.Segments) {
                var cloned = segment;
                cloned.Offset += dataOffset;
                writer.WriteElf32(cloned);
            }
            writer.Write(file.Data.ToArray());
            foreach (var section in file.Sections) {
                var cloned = section;
                if (section.Type != ElfSectionType.Null) {
                    cloned.Offset += dataOffset;
                }
                writer.WriteElf32(cloned);
            }
        }

        private void AddCode(ElfFile file, AsmContext context) {
            if (context.CodeSection.VirtualSize > 0) {
                file.Sections.Add(new ElfSection {
                    Name = file.Strings.SaveString(".text"),
                    Type = ElfSectionType.ProgBits,
                    Address = 0,
                    Flags = ElfSectionFlags.Alloc | ElfSectionFlags.Executable,
                    Size = (uint)context.CodeSection.BytesCount,
                    Align = 2,
                    Offset = (uint)file.Data.Position
                });
                file.Segments.Add(new ElfSegment {
                    Type = ElfSegmentType.Load,
                    Offset = (uint)file.Data.Position,
                    VirtualAddress = 0,
                    PhysicalAddress = 0,
                    FileSize = (uint)context.CodeSection.BytesCount,
                    MemorySize = (uint)context.CodeSection.VirtualSize,
                    Flags = ElfSegmentFlags.Executable | ElfSegmentFlags.Readable,
                    Align = 1
                });
                file.Data.Write(context.CodeSection.Content.ToArray(), 0, context.CodeSection.BytesCount);
            }
        }

        private void AddData(ElfFile file, AsmContext context) {
            if (context.DataSection.VirtualSize > 0) {
                file.Sections.Add(new ElfSection {
                    Name = file.Strings.SaveString(".bss"),
                    Type = ElfSectionType.NoBits,
                    Address = 0x800060,
                    Flags = ElfSectionFlags.Alloc | ElfSectionFlags.Writeable,
                    Size = (uint)context.DataSection.BytesCount,
                    Align = 1,
                    Offset = (uint)file.Data.Position
                });
                file.Segments.Add(new ElfSegment {
                    Type = ElfSegmentType.Load,
                    Offset = (uint)file.Data.Position,
                    VirtualAddress = 0x800060,
                    PhysicalAddress = 0x800060,
                    FileSize = (uint)context.DataSection.BytesCount,
                    MemorySize = (uint)context.DataSection.VirtualSize,
                    Flags = ElfSegmentFlags.Writeable | ElfSegmentFlags.Readable,
                    Align = 1,
                });
                file.Data.Write(context.DataSection.Content.ToArray(), 0, context.DataSection.BytesCount);
            }
        }

        private void AddFlash(ElfFile file, AsmContext context) {
            if (context.FlashSection.VirtualSize > 0) {
                file.Sections.Add(new ElfSection {
                    Name = file.Strings.SaveString(".flash"),
                    Type = ElfSectionType.ProgBits,
                    Address = 0x810000,
                    Flags = ElfSectionFlags.Alloc | ElfSectionFlags.Writeable,
                    Size = (uint)context.FlashSection.BytesCount,
                    Align = 1,
                    Offset = (uint)file.Data.Position
                });
                file.Segments.Add(new ElfSegment {
                    Type = ElfSegmentType.Load,
                    Offset = (uint)file.Data.Position,
                    VirtualAddress = 0x810000,
                    PhysicalAddress = 0x810000,
                    FileSize = (uint)context.FlashSection.BytesCount,
                    MemorySize = (uint)context.FlashSection.VirtualSize,
                    Flags = ElfSegmentFlags.Writeable | ElfSegmentFlags.Readable,
                    Align = 1,
                });
                file.Data.Write(context.FlashSection.Content.ToArray(), 0, context.FlashSection.BytesCount);
            }
        }
    }
}
