namespace Atmega.Flasher.Hex {
    public class HexBoardLine {

        public int Address { get; set; }

        public readonly HexBoardByte[] Bytes = new HexBoardByte[16];

        public HexBoardLine() {
            for (var i = 0; i < Bytes.Length; i++) {
                Bytes[i] = new HexBoardByte();
            }
        }

    }
}
