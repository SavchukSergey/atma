namespace Atmega.Flasher {
    public struct AtmegaLockBits {

        public MemoryProtectionMode MemoryLockBits;

        public MemoryProtectionMode ApplicationLockBits;

        public MemoryProtectionMode BootLoaderLockBits;

        public byte Value {
            get {
                var res = 0;
                res |= MemoryLockBits.AllowRead ? 0x01 : 0x00;
                res |= MemoryLockBits.AllowWrite ? 0x02 : 0x00;
                res |= ApplicationLockBits.AllowRead ? 0x04 : 0x00;
                res |= ApplicationLockBits.AllowWrite ? 0x08 : 0x00;
                res |= BootLoaderLockBits.AllowRead ? 0x10 : 0x00;
                res |= BootLoaderLockBits.AllowWrite ? 0x20 : 0x00;
                return (byte)res;
            }
            set {
                MemoryLockBits.AllowRead = (value & 0x01) != 0;
                MemoryLockBits.AllowWrite = (value & 0x02) != 0;
                ApplicationLockBits.AllowRead = (value & 0x04) != 0;
                ApplicationLockBits.AllowWrite = (value & 0x08) != 0;
                BootLoaderLockBits.AllowRead = (value & 0x10) != 0;
                BootLoaderLockBits.AllowWrite = (value & 0x20) != 0;
            }
        }

    }
}
