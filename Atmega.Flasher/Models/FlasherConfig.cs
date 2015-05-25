namespace Atmega.Flasher.Models {
    public class FlasherConfig {

        private readonly FlasherAvrIspConfig _avrisp = new FlasherAvrIspConfig();

        public FlasherAvrIspConfig AvrIsp {
            get { return _avrisp; }
        }

        public void Reload() {
            _avrisp.Reload();
        }
    }
}
