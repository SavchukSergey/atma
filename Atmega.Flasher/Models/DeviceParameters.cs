using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Atmega.Flasher.Models {
    public class DeviceParameters {

        public string Name { get; set; }

        public int FlashSize { get; set; }

        public int EepromSize { get; set; }

        public int RamSize { get; set; }

        public AvrSignature Signature { get; set; }

        public static DeviceParameters From(XElement node) {
            return new DeviceParameters {
                Name = node.Attribute("name").Value,
                FlashSize = int.Parse(node.Attribute("flash").Value),
                EepromSize = int.Parse(node.Attribute("eeprom").Value),
                RamSize = int.Parse(node.Attribute("ram").Value),
                Signature = AvrSignature.Parse(node.Attribute("signature").Value)
            };
        }

        public static IList<DeviceParameters> List() {
            var xDoc = XDocument.Load("devices.xml");
            return xDoc.Root.Elements().Select(From).ToList();
        }
    }
}
