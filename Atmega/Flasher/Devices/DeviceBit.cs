﻿using System.Xml.Linq;

namespace Atmega.Flasher.Devices {
    public class DeviceBit {

        public int Address { get; set; }

        public int Bit { get; set; }

        public string Name { get; set; }

        public bool Inverse { get; set; }

        public bool? Constant { get; set; }

        public bool Value { get; set; }

        public static DeviceBit From(XElement xDeviceBit) {
            var xAddress = xDeviceBit.Attribute("address");
            var xBit = xDeviceBit.Attribute("bit");
            var xName = xDeviceBit.Attribute("name");
            var xInverse = xDeviceBit.Attribute("inverse");
            var xConstant = xDeviceBit.Attribute("constant");
            return new DeviceBit {
                Address = xAddress != null ? int.Parse(xAddress.Value) : 0,
                Bit = xBit != null ? int.Parse(xBit.Value) : 0,
                Name = xName != null ? xName.Value : "",
                Inverse = xInverse != null && xInverse.Value.ToLowerInvariant() == "true",
                Constant = xConstant != null ? new bool?(xConstant.Value == "1") : null
            };
        }

        public byte Apply(byte value) {
            return Constant.HasValue ? ApplyRaw(value, Constant.Value) : ApplyRaw(value, Value ^ Inverse);
        }

        private byte ApplyRaw(byte source, bool rawValue) {
            var mask = 1 << Bit;
            if (rawValue) {
                return (byte)(source | mask);
            }
            return (byte)(source & (~mask));
        }

        public override string ToString() {
            return Name;
        }
    }
}