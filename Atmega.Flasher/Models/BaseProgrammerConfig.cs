namespace Atmega.Flasher.Models {
    public abstract class BaseProgrammerConfig : BaseConfig {
        
        protected BaseProgrammerConfig(string keyPrefix)
            : base(keyPrefix) {
        }

        public abstract IProgrammer CreateProgrammer(DeviceInfo device);
    }
}
