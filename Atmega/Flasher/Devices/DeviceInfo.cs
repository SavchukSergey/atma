using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Atmega.Flasher.Devices {
    public class DeviceInfo {

        private DeviceFlashParameters _flash = new DeviceFlashParameters();
        private DeviceEepromParameters _eeprom = new DeviceEepromParameters();
        private DeviceBits _lockBits = new DeviceBits();
        private DeviceBits _fuseBits = new DeviceBits();

        public string Name { get; set; }

        public DeviceFlashParameters Flash {
            get { return _flash; }
        }

        public DeviceEepromParameters Eeprom {
            get { return _eeprom; }
        }

        public DeviceBits LockBits {
            get { return _lockBits; }
        }

        public DeviceBits FuseBits {
            get { return _fuseBits; }
        }

        public int RamSize { get; set; }

        public AvrSignature Signature { get; set; }

        public static DeviceInfo From(XElement node) {
            var xName = node.Attribute("name");

            var xFlash = node.Element("flash");
            var xEeprom = node.Element("eeprom");
            var xRam = node.Attribute("ram");
            var xSignature = node.Attribute("signature");
            var xLockBits = node.Element("lockBits");
            var xFuseBits = node.Element("fuseBits");
            return new DeviceInfo {
                Name = xName != null ? xName.Value : "unknown",
                _flash = xFlash != null ? DeviceFlashParameters.From(xFlash) : new DeviceFlashParameters(),
                _eeprom = xEeprom != null ? DeviceEepromParameters.From(xEeprom) : new DeviceEepromParameters(),
                RamSize = xRam != null ? int.Parse(xRam.Value) : 0,
                Signature = xSignature != null ? AvrSignature.Parse(xSignature.Value) : new AvrSignature(),
                _lockBits = xLockBits != null ? DeviceBits.Parse(xLockBits) : new DeviceBits(),
                _fuseBits = xFuseBits != null ? DeviceBits.Parse(xFuseBits) : new DeviceBits(),
            };
        }

        public static IList<DeviceInfo> List() {
            var xDoc = XDocument.Load("devices.xml");
            return xDoc.Root.Elements().Select(From).OrderBy(item => item.Name).ToList();
        }
    }

}
