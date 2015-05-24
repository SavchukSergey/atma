namespace Atmega.Flasher.Hex {
    public class HexBoardByte {

        public byte? Value { get; set; }

        public static implicit operator HexBoardByte(byte? val) {
            return new HexBoardByte { Value = val };
        }

    }
}
