namespace Atmega.Flasher.Models {
    public class DeviceParameters {

        public int FlashSize { get; set; }

        public int EepromSize { get; set; }

        public int RamSize { get; set; }

        public AvrSignature Signature { get; set; }

        public static DeviceParameters Get(string deviceName) {
            switch (deviceName.ToLower()) {
                case "atmega48a":
                    return new DeviceParameters { FlashSize = 4096, EepromSize = 256, RamSize = 512, Signature = new AvrSignature(0x1e, 0x92, 0x05)};
                case "atmega48pa":
                    return new DeviceParameters { FlashSize = 4096, EepromSize = 256, RamSize = 512, Signature = new AvrSignature(0x1e, 0x92, 0x0a) };
                case "atmega88a":
                    return new DeviceParameters { FlashSize = 8192, EepromSize = 512, RamSize = 1024, Signature = new AvrSignature(0x1e, 0x93, 0x0a) };
                case "atmega88pa":
                    return new DeviceParameters { FlashSize = 8192, EepromSize = 512, RamSize = 1024, Signature = new AvrSignature(0x1e, 0x93, 0x0f) };
                case "atmega168a":
                    return new DeviceParameters { FlashSize = 16384, EepromSize = 512, RamSize = 1024, Signature = new AvrSignature(0x1e, 0x94, 0x06) };
                case "atmega168pa":
                    return new DeviceParameters { FlashSize = 16384, EepromSize = 512, RamSize = 1024, Signature = new AvrSignature(0x1e, 0x94, 0x0b) };
                case "atmega328":
                    return new DeviceParameters { FlashSize = 32768, EepromSize = 1024, RamSize = 2048, Signature = new AvrSignature(0x1e, 0x95, 0x14) };
                case "atmega328p":
                    return new DeviceParameters { FlashSize = 32768, EepromSize = 1024, RamSize = 2048, Signature = new AvrSignature(0x1e, 0x95, 0x0f) };
                default:
                    return null;
            }
        }
    }
}
