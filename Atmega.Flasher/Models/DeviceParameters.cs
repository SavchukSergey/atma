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
            var xName = node.Attribute("name");
            var xFlashSize = xFlash != null ? xFlash.Attribute("size") : null;
            var xFlashPage = xFlash != null ? xFlash.Attribute("page") : null;
            var xEeprom = node.Attribute("eeprom");
            var xRam = node.Attribute("ram");
            var xSignature = node.Attribute("signature");
            return new DeviceParameters {
                Name = xName != null ? xName.Value : "unknown",
                Flash = {
                    Size = xFlashSize != null ? int.Parse(xFlashSize.Value) : 0,
                    PageSize = xFlashSize != null ? int.Parse(xFlashPage.Value) : 0,
                },
                EepromSize = xEeprom != null ? int.Parse(xEeprom.Value) : 0,
                RamSize = xRam != null ? int.Parse(xRam.Value) : 0,
                Signature = xSignature != null ? AvrSignature.Parse(xSignature.Value) : new AvrSignature()
            };
        }

        public static IList<DeviceParameters> List() {
            var xDoc = XDocument.Load("devices.xml");
            return xDoc.Root.Elements().Select(From).OrderBy(item => item.Name).ToList();
        }
    }

    public class DeviceFlashParameters {

        public int Size { get; set; }

        public int PageSize { get; set; }

    }
}
