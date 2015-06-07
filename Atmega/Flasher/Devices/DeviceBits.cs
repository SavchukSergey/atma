using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Linq;

namespace Atmega.Flasher.Devices {
    public class DeviceBits {

        private readonly IList<DeviceBit> _bits = new List<DeviceBit>();

        public IList<DeviceBit> Bits {
            get { return _bits; }
        }

        public IList<DeviceByte> ToBytes() {
            var res = new Dictionary<int, DeviceByte>();
            foreach (var bit in _bits) {
                var adr = bit.Address;
                DeviceByte item;
                res.TryGetValue(adr, out item);
                item.Value = bit.Apply(item.Value);
                res[adr] = item;
            }
            return res.Values.ToList();
        }

        public static DeviceBits Parse(XElement xBits) {
            var res = new DeviceBits();
            foreach (var xBit in xBits.Elements()) {
                res.Bits.Add(DeviceBit.From(xBit));
            }
            return res;
        }
    }
}
