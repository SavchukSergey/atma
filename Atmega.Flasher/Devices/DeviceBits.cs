using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Atmega.Flasher.Devices {
    public class DeviceBits {

        private readonly IList<DeviceBit> _bits = new List<DeviceBit>();

        public IList<DeviceBit> Bits {
            get { return _bits; }
        }

        public int StartAddress {
            get {
                return _bits.Count == 0 ? 0 : _bits.Min(item => item.Address);
            }
        }

        public int EndAddress {
            get {
                return _bits.Count == 0 ? 0 : _bits.Max(item => item.Address);
            }
        }

        public IList<DeviceByteBits> BitsByAddress {
            get {
                return _bits
                    .GroupBy(item => item.Address)
                    .Select(item => new DeviceByteBits {
                        Address = item.Key,
                        Bits = item.ToList()
                    })
                    .ToList();
            }
        }

        public int Size {
            get {
                return EndAddress - StartAddress + 1;
            }
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

        public void ApplyFrom(byte[] data) {
            foreach (var bit in _bits) {
                if (bit.Address < data.Length) {
                    var bt = data[bit.Address];
                    bit.GetValueFrom(bt);
                }
            }
        }
    }
}
