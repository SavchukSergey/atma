using System.Collections.Generic;
using System.IO;

namespace Atmega.Elf {
    public class ElfFile {

        private readonly List<ElfSection> _sections = new List<ElfSection>();
        private readonly List<ElfSegment> _segments = new List<ElfSegment>();
        private readonly ElfStrings _strings = new ElfStrings();

        private readonly MemoryStream _data = new MemoryStream();

        public List<ElfSection> Sections {
            get { return _sections; }
        }

        public List<ElfSegment> Segments {
            get { return _segments; }
        }

        public Stream Data {
            get { return _data; }
        }

        public ElfStrings Strings {
            get { return _strings; }
        }


        public int AddStringsSection() {
            _sections.Add(new ElfSection {
                Name = _strings.SaveString(".shstrtab"),
                Type = ElfSectionType.StrTab,
                Address = 0,
                Flags = ElfSectionFlags.None,
                Size = (uint)_strings.BytesSize,
                Align = 1,
                Offset = (uint)Data.Position
            });
            _data.Write(_strings.ToArray(), 0, _strings.BytesSize);
            return _sections.Count - 1;
        }
    }
}
