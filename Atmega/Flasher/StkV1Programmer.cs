using System;
using Atmega.Flasher.STKv1;

namespace Atmega.Flasher {
    public class StkV1Programmer : IProgrammer {

        private readonly StkV1Client _client;
        private readonly DeviceInfo _device;
        private const int BLOCK_SIZE = 1024;

        public StkV1Programmer(StkV1Client client, DeviceInfo device) {
            _client = client;
            _device = device;
        }

        public void Start() {
            _client.Open();
            _client.ResetDevice();
            _client.GetSyncLoop();
            _client.SetDeviceParameters(new StkV1DeviceParameters {
                DeviceCode = (StkDeviceCode)0x86,
                Revision = 0,
                ProgType = 0,
                ParMode = 1,
                Polling = 1,
                SelfTimed = 1,
                LockBytes = 1,
                FuseBytes = 3,
                FlashPollVal1 = 0xff,
                FlashPollVal2 = 0xff,
                EepromPollVal1 = 0xff,
                EepromPollVal2 = 0xff,
                PageSize = (ushort) _device.Flash.PageSize,
                EepromPageSize = (ushort) _device.EepromSize,
                FlashSize = (uint) _device.Flash.Size
            });
            _client.SetDeviceParametersExt(new StkV1DeviceParametersExt {
                EepromPageSize = 4,
                SignalPageL = 0xd7,
                SignalBs2 = 0xc2,
                ResetDisable = 0
            });
            _client.EnterProgramMode();
        }

        public void Stop() {
            _client.LeaveProgramMode();
            _client.Close();
        }

        public void ReadPage(int address, AvrMemoryType memType, byte[] data, int dataStart, int dataLength) {
            switch (memType) {
                case AvrMemoryType.Eeprom:
                    ReadEeprom(address, data, dataStart, dataLength);
                    break;
                case AvrMemoryType.Flash:
                    ReadFlash(address, data, dataStart, dataLength);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public void WritePage(int address, AvrMemoryType memType, byte[] data, int dataStart, int dataLength) {
            switch (memType) {
                case AvrMemoryType.Eeprom:
                    WriteEeprom(address, data, dataStart, dataLength);
                    break;
                case AvrMemoryType.Flash:
                    WriteFlash(address, data, dataStart, dataLength);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public AtmegaLockBits ReadLockBits() {
            return new AtmegaLockBits { Value = _client.Universal(0x58, 0x00, 0x00, 0x00) };
        }

        public void WriteLockBits(AtmegaLockBits bits) {
            _client.Universal(0xac, 0xe0, 0x00, bits.Value);
        }

        public void EraseDevice() {
            _client.Universal(0xac, 0x80, 0x00, 0x00);
        }

        private void WriteEeprom(int address, byte[] data, int dataStart, int dataLength) {
            var offset = address;
            var end = address + dataLength;
            while (offset < end) {
                _client.LoadAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);

                _client.ProgramPage(data, offset - address + dataStart, cnt, AvrMemoryType.Eeprom);
                offset += cnt;
            }
        }

        private void ReadEeprom(int address, byte[] data, int dataStart, int dataLength) {
            var offset = address;
            var end = address + dataLength;
            while (offset < end) {
                _client.LoadAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);
                _client.ReadPage(data, offset - address + dataStart, cnt, AvrMemoryType.Eeprom);
                offset += cnt;
            }
        }

        private void WriteFlash(int start, byte[] data, int dataStart, int dataLength) {
            var offset = start;
            var end = start + dataLength;
            while (offset < end) {
                _client.LoadAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);

                _client.ProgramPage(data, offset - start + dataStart, cnt, AvrMemoryType.Flash);
                offset += cnt;
            }
        }

        private void ReadFlash(int address, byte[] data, int dataStart, int dataLength) {
            var offset = address;
            var end = address + dataLength;
            while (offset < end) {
                _client.LoadAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);
                _client.ReadPage(data, offset - address + dataStart, cnt, AvrMemoryType.Flash);
                offset += cnt;
            }
        }

        public void Dispose() {
            _client.Dispose();
        }
    }
}
