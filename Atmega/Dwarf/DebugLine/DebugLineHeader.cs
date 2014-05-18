namespace Atmega.Dwarf.DebugLine {
    public struct DebugLineHeader {

        public uint UnitLength;

        public ushort Version;

        public uint HeaderLength;

        public byte MinimumInstructionLength;

        public byte MaximumInstructionLength;

        public byte DefaultIsStatement;

        public sbyte LineBase;

        public byte LineRange;

        public byte OpcodeBase;

        public int[] OpcodesArgsCount;

        public string[] IncludeDirectories;

        public DebugLineFile[] FileNames;

    }
}
