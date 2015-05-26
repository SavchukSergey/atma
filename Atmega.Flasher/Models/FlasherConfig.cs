namespace Atmega.Flasher.Models {
    public class FlasherConfig : BaseConfig {

        public FlasherAvrIspConfig AvrIsp { get; protected set; }

        public FlasherConfig() {
            AvrIsp = new FlasherAvrIspConfig();
        }

        public static FlasherConfig ReadFromConfig() {
            return new FlasherConfig {
                AvrIsp = FlasherAvrIspConfig.ReadFromConfig()
            };
        }

        protected override string KeyPrefix {
            get { return null; }
        }

        public override void Save() {
            AvrIsp.Save();
        }
    }
}
