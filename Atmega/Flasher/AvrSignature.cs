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

    }
}
