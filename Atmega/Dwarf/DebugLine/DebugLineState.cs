namespace Atmega.Dwarf.DebugLine {
    public class DebugLineState {

        public uint Address;

        public int OperationIndex;

        public uint File;

        public uint Line;

        public uint Column;

        public bool IsStatement;
        public bool BasicBlock;
        public bool EndSequence;
        public bool PrologueEnd;
        public bool EpilogueBegin;
        public uint Isa;
        public uint Discrimintator;

    }
}
