using System.Collections.Generic;

namespace Atmega.Flasher.Devices {
    public class DeviceByteBits {

        public int Address { get; set; }

        public IList<DeviceBit> Bits { get; set; }

    }
}
