using System;

namespace Atmega.Flasher {
    public class DeviceOperation {

        public virtual int FlashDone { get; set; }

        public virtual int EepromDone { get; set; }

        public virtual int FlashSize { get; set; }

        public virtual int EepromSize { get; set; }

        public int Done {
            get { return FlashDone + EepromDone; }
        }

        public int Total {
            get { return FlashSize + EepromSize; }
        }

        public double Progress {
            get { return 100.0 * Done / Math.Max(1, Total); }
        }

        public virtual string CurrentState { get; set; }
        
        public void IncrementDone(int count, AvrMemoryType memType) {
            switch (memType) {
                case AvrMemoryType.Flash:
                    FlashDone += count;
                    break;
                case AvrMemoryType.Eeprom:
                    EepromDone += count;
                    break;
            }
        }

    }
}
