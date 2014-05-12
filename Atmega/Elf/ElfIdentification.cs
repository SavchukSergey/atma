﻿namespace Atmega.Elf {
    public struct ElfIdentification {

        public char[] Magic;

        public ElfFileClass FileClass;

        public ElfDataType DataType;

        public byte Version;
    }
}
