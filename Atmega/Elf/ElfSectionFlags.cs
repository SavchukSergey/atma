using System;

namespace Atmega.Elf {
    [Flags]
    public enum ElfSectionFlags {
        None = 0,
        Writeable = 0x01,
        Alloc = 0x02,
        Executable = 0x04,
    }
}
