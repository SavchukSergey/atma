using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Atmega.Flasher.Hex;

namespace Atmega.Flasher.Devices {
    public class DeviceBits {

        private readonly IList<DeviceBitsGroup> _groups = new List<DeviceBitsGroup>();

        public IList<DeviceBitsGroup> Groups {
            get { return _groups; }
        }

        public int StartAddress {
            get {
                return _groups.Count == 0 ? 0 : _groups.Min(item => item.StartAddress);
            }
        }

        public int EndAddress {
            get {
                return _groups.Count == 0 ? 0 : _groups.Max(item => item.EndAddress);
            }
        }

        public int Size {
            get {
                return EndAddress - StartAddress + 1;
            }
        }

        public int PageSize { get; set; }

        public static DeviceBits Parse(XElement xBits) {
            var res = new DeviceBits();

            var xPage = xBits.Attribute("page");
            res.PageSize = xPage != null ? int.Parse(xPage.Value) : 1;
            foreach (var xBit in xBits.Elements()) {
                res.Groups.Add(DeviceBitsGroup.From(xBit));
            }
            return res;
        }

        public void ApplyFrom(HexBoard data) {
            foreach (var gr in _groups) {
                gr.ApplyFrom(data);
            }
        }

        public void ApplyTo(HexBoard board) {
            foreach (var gr in _groups) {
                gr.ApplyTo(board);
            }
        }


    }
}
