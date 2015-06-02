using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Atmega.Flasher.Models {
    public class DeviceParameters {

        private readonly DeviceFlashParameters _flash = new DeviceFlashParameters();

        public string Name { get; set; }

        public DeviceFlashParameters Flash {
            get { return _flash; }
        }

        public int EepromSize { get; set; }

        public int RamSize { get; set; }

        public AvrSignature Signature { get; set; }

        public static DeviceParameters From(XElement node) {
            var xFlash = node.Element("flash");
            return new DeviceParameters {
                Name = node.Attribute("name").Value,
                Flash = {
                    Size = int.Parse(xFlash.Attribute("size").Value),
                    PageSize = int.Parse(xFlash.Attribute("page").Value),
                },
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

    public class DeviceFlashParameters {

        public int Size { get; set; }

        public int PageSize { get; set; }

    }
}
