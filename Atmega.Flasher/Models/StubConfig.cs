using Atmega.Flasher.IO;

namespace Atmega.Flasher.Models {
    public class StubConfig : BaseProgrammerConfig {
        public StubConfig(string keyPrefix)
            : base(keyPrefix) {
        }

        public override void Save() {
        }

        public override void ReadFromConfig() {
        }

        public override IProgrammer CreateProgrammer(DeviceInfo device) {
            return new StubProgrammer();
        }

        public override IAvrChannel CreateChannel() {
            return new StubChannel();
        }
    }
}
