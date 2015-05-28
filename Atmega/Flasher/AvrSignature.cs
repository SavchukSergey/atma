using System.Globalization;
using System.Xml.Linq;

namespace Atmega.Flasher {
    public class AvrSignature {

        public AvrSignature() {
        }

        public AvrSignature(byte vendor, byte middle, byte low) {
            Vendor = vendor;
            Middle = middle;
            Low = low;
        }

        public byte Vendor { get; set; }

        public byte Middle { get; set; }

        public byte Low { get; set; }

        public static AvrSignature Parse(string val) {
            var all = int.Parse(val, NumberStyles.HexNumber);
            return new AvrSignature((byte)(all >> 16), (byte)(all >> 8), (byte)all);
        }
    }
}
