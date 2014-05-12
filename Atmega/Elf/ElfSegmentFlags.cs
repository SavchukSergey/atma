using System;

namespace Atmega.Elf {
    [Flags]
    public enum ElfSegmentFlags {
        Executable = 1,
        Writeable = 2,
        Readable = 4
    }
}
