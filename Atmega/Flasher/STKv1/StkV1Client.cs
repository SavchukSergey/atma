﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Atmega.Flasher.AvrIsp;
using Atmega.Flasher.IO;

namespace Atmega.Flasher.STKv1 {
    public class StkV1Client : IDisposable {

        private const byte STK_OK = 0x10;
        private const byte STK_FAILED = 0x00;
        private const byte STK_INSYNC = 0x14;
        private const byte STK_NOSYNC = 0x00;
        private const byte STK_UNKNOWN = 0x00;
        private const byte STK_N_DEVICE = 0x00;
        private const byte CRC_EOP = 0x20;

        private readonly IAvrChannel _port;

        private ushort _position;

        public StkV1Client(IAvrChannel port) {
            _port = port;
        }


        public string GetSignOn() {
            WriteCommand(StkV1Command.GetSignOn);
            WriteCrcEop();

            AssertInSync();
            var res = new List<byte>();
            do {
                var ch = _port.ReceiveByte();
                if (ch == (int)StkV1Response.Ok) break;
                res.Add(ch);
            } while (true);

            return Encoding.ASCII.GetString(res.ToArray());
        }

        public void GetSynchronization() {
            WriteCommand(StkV1Command.GetSync);
            WriteCrcEop();

            AssertInSync();
            AssertOk();
        }

        public byte GetParameterValue(StkV1Parameter param) {
            WriteCommand(StkV1Command.GetParameterValue);
            WriteByte((byte)param);
            WriteCrcEop();

            AssertInSync();
            var res = _port.ReceiveByte();
            AssertOk();
            return res;
        }

        public void SetParameterValue(StkV1Parameter param, byte value) {
            WriteCommand(StkV1Command.SetParameterValue);
            WriteByte((byte)param);
            WriteByte(value);
            WriteCrcEop();

            AssertInSync();
            AssertOk();
        }

        public void SetDeviceParameters(StkV1DeviceParameters parameters) {
            WriteCommand(StkV1Command.SetDeviceParameters);
            WriteByte(parameters.DeviceCode);
            WriteByte(parameters.Revision);
            WriteByte(parameters.ProgType);
            WriteByte(parameters.ParMode);
            WriteByte(parameters.Polling);
            WriteByte(parameters.SelfTimed);
            WriteByte(parameters.LockBytes);
            WriteByte(parameters.FuseBytes);
            WriteByte(parameters.FlashPollVal1);
            WriteByte(parameters.FlashPollVal2);
            WriteByte(parameters.EepromPollVal1);
            WriteByte(parameters.EepromPollVal2);
            WriteByte(parameters.PageSizeHigh);
            WriteByte(parameters.PageSizeLow);
            WriteByte(parameters.EepromSizeHigh);
            WriteByte(parameters.EepromSizeLow);
            WriteByte(parameters.FlashSize4);
            WriteByte(parameters.FlashSize3);
            WriteByte(parameters.FlashSize2);
            WriteByte(parameters.FlashSize1);
            WriteCrcEop();

            AssertInSync();
            AssertOk();
        }

        public void SetDeviceParameters(StkV1DeviceParametersExt parameters) {
            WriteCommand(StkV1Command.SetDeviceParametersExt);
            WriteByte(4);
            WriteByte(parameters.EepromPageSize);
            WriteByte(parameters.SignalPageL);
            WriteByte(parameters.SignalBs2);
            WriteByte(parameters.ResetDisable);
            WriteCrcEop();

            AssertInSync();
            AssertOk();
        }

        public void EnterProgramMode() {
            WriteCommand(StkV1Command.EnterProgramMode);
            WriteCrcEop();

            Thread.Sleep(200);
            AssertInSync();
            AssertOk();
        }

        public void LeaveProgramMode() {
            WriteCommand(StkV1Command.LeaveProgramMode);
            WriteCrcEop();
            AssertInSync();
            AssertOk();
        }

        public void ChipErase() {
            WriteCommand(StkV1Command.ChipErase);
            WriteCrcEop();
            AssertInSync();
            AssertOk();
        }

        public void LoadAddress(ushort val) {
            WriteCommand(StkV1Command.LoadAddress);
            for (var i = 0; i < 2; i++) {
                var bt = (byte)(val & 0xff);
                _port.SendByte(bt);
                val >>= 8;
            }
            WriteCrcEop();
            AssertInSync();
            AssertOk();
        }

        public void ProgramFlashMemory(ushort flashCommand) {
            WriteCommand(StkV1Command.ProgramFlashMemory);
            for (var i = 0; i < 2; i++) {
                var bt = (byte)(flashCommand & 0xff);
                _port.SendByte(bt);
                flashCommand >>= 8;
            }
            WriteCrcEop();
            AssertInSync();
            AssertOk();
        }

        public void ProgramDataMemory(byte val) {
            WriteCommand(StkV1Command.ProgramDataMemory);
            WriteByte(val);
            WriteCrcEop();
            AssertInSync();
            AssertOk();
        }

        public void ProgramFuseBits(byte fuseBitsLow, byte fuseBitsHigh) {
            WriteCommand(StkV1Command.ProgramFuseBits);
            _port.SendByte(fuseBitsLow);
            _port.SendByte(fuseBitsHigh);
            WriteCrcEop();
            AssertInSync();
            AssertOk();
        }

        public void ProgramFuseBitsExt(byte fuseBitsLow, byte fuseBitsHigh, byte fuseBitsExt) {
            WriteCommand(StkV1Command.ProgramFuseBitsExt);
            _port.SendByte(fuseBitsLow);
            _port.SendByte(fuseBitsHigh);
            _port.SendByte(fuseBitsExt);
            WriteCrcEop();
            AssertInSync();
            AssertOk();
        }

        public void ProgramLockBits(byte lockBits) {
            WriteCommand(StkV1Command.ProgramLockBits);
            _port.SendByte(lockBits);
            WriteCrcEop();
            AssertInSync();
            AssertOk();
        }

        public void ProgramPage(byte[] data, int dataStart, int dataLength, AvrMemoryType memType) {
            WriteCommand(StkV1Command.ProgramPage);
            WriteByte((byte)(dataLength >> 8));
            WriteByte((byte)(dataLength & 0xff));
            switch (memType) {
                case AvrMemoryType.Flash:
                    WriteChar('F');
                    break;
                case AvrMemoryType.Eeprom:
                    WriteChar('E');
                    break;
                default:
                    throw new NotSupportedException();
            }
            for (var i = 0; i < dataLength; i++) {
                _port.SendByte(data[i + dataStart]);
            }
            WriteCrcEop();
            AssertInSync();
            AssertOk();
        }

        public void ReadFlashMemory(byte[] data, int dataStart, int dataLength) {
            WriteCommand(StkV1Command.ReadFlashMemory);
            WriteByte((byte)(dataLength >> 8));
            WriteByte((byte)(dataLength & 0xff));
            WriteCrcEop();
            AssertInSync();

            for (var i = 0; i < dataLength; i++) {
                data[i + dataStart] = _port.ReceiveByte();
            }
            AssertOk();
        }

        public void ReadDataMemory(byte[] data, int dataStart, int dataLength) {
            WriteCommand(StkV1Command.ReadDataMemory);
            WriteByte((byte)(dataLength >> 8));
            WriteByte((byte)(dataLength & 0xff));
            WriteCrcEop();
            AssertInSync();

            for (var i = 0; i < dataLength; i++) {
                data[i + dataStart] = _port.ReceiveByte();
            }
            AssertOk();
        }

        public StkFuseBits ReadFuseBits() {
            WriteCommand(StkV1Command.ReadFuseBits);
            WriteCrcEop();
            AssertInSync();

            StkFuseBits res;
            res.Low = _port.ReceiveByte();
            res.High = _port.ReceiveByte();
            AssertOk();

            return res;
        }

        public StkFuseBitsExt ReadFuseBitsExt() {
            WriteCommand(StkV1Command.ReadFuseBitsExt);
            WriteCrcEop();
            AssertInSync();

            StkFuseBitsExt res;
            res.Low = _port.ReceiveByte();
            res.High = _port.ReceiveByte();
            res.Extended = _port.ReceiveByte();
            AssertOk();

            return res;
        }

        public byte ReadLockBits() {
            WriteCommand(StkV1Command.ReadLockBits);
            WriteCrcEop();
            AssertInSync();

            var res = _port.ReceiveByte();
            AssertOk();

            return res;
        }

        public void ReadPage(byte[] data, int dataStart, int dataLength, AvrMemoryType memType) {
            WriteCommand(StkV1Command.ReadPage);
            WriteByte((byte)(dataLength >> 8));
            WriteByte((byte)(dataLength & 0xff));
            switch (memType) {
                case AvrMemoryType.Flash:
                    WriteChar('F');
                    break;
                case AvrMemoryType.Eeprom:
                    WriteChar('E');
                    break;
                default:
                    throw new NotSupportedException();
            }
            WriteCrcEop();
            AssertInSync();

            for (var i = 0; i < dataLength; i++) {
                data[i + dataStart] = _port.ReceiveByte();
            }
            AssertOk();
        }

        public AvrSignature ReadSignatureBytes() {
            WriteCommand(StkV1Command.ReadSignatureBytes);
            WriteCrcEop();
            AssertInSync();

            AvrSignature res;
            res.Vendor = _port.ReceiveByte();
            res.Middle = _port.ReceiveByte();
            res.Low = _port.ReceiveByte();
            AssertOk();

            return res;
        }

        public byte ReadOscillatorCalibrationByte() {
            WriteCommand(StkV1Command.ReadOscillatorCalibrationByte);
            WriteCrcEop();
            AssertInSync();

            var res = _port.ReceiveByte();
            AssertOk();

            return res;
        }

        public byte ReadOscillatorCalibrationByteExt(byte adr) {
            WriteCommand(StkV1Command.ReadOscillatorCalibrationByteExt);
            _port.SendByte(adr);
            WriteCrcEop();
            AssertInSync();

            var res = _port.ReceiveByte();
            AssertOk();

            return res;
        }

        public byte Universal(byte a, byte b, byte c, byte d) {
            WriteCommand(StkV1Command.Universal);
            _port.SendByte(a);
            _port.SendByte(b);
            _port.SendByte(c);
            _port.SendByte(d);
            WriteCrcEop();

            AssertInSync();
            var res = _port.ReceiveByte();
            AssertOk();

            return res;
        }

        public byte UniversalExt(byte[] cmd) {
            WriteCommand(StkV1Command.UniversalExt);
            _port.SendByte((byte)(cmd.Length - 1));
            foreach (var bt in cmd) {
                _port.SendByte(bt);
            }
            WriteCrcEop();

            AssertInSync();
            var res = _port.ReceiveByte();
            AssertOk();

            return res;
        }

        public void ResetDevice() {
            _port.ToggleReset(true);
            Thread.Sleep(200);
            _port.ToggleReset(false);
            Thread.Sleep(200);
            _port.ToggleReset(true);
            Thread.Sleep(200);
            //_port.DtrEnable = true; //in-circuit capacity will break dtr signal. so we can skip dtr=true
        }

        public void Open() {
            _port.Open();
        }



        private byte BRead() {
            WriteByte(CRC_EOP);
            ReadAssert(STK_INSYNC);
            var res = ReadByte();
            ReadAssert(STK_OK);
            return res;
        }

        public byte GetVersion(byte arg) {
            WriteChar('A');
            WriteByte(arg);
            return BRead();
        }

        public StkVersion ReadVersion() {
            return new StkVersion {
                Hardware = GetVersion(0x80),
                SoftwareMajor = GetVersion(0x81),
                SoftwareMinor = GetVersion(0x82),
                Type = (char)GetVersion(0x93)
            };
        }

        public void Close() {
            _port.Close();
        }

        private void ReadEmpty() {
            WriteByte(CRC_EOP);
            ReadAssert(STK_INSYNC);
            ReadAssert(STK_OK);
        }

        private byte ReadByte() {
            return _port.ReceiveByte();
        }

        private void WriteChar(char ch) {
            WriteByte((byte)ch);
        }

        private void WriteByte(byte bt) {
            _port.SendByte(bt);
        }

        private void WriteCommand(StkV1Command command) {
            _port.SendByte((byte)command);
        }

        public void Dispose() {
            _port.Dispose();
        }

        private void ReadAssert(byte bt) {
            var res = _port.ReceiveByte();
            if (res != bt) throw new Exception("nosync");
        }

        private void AssertInSync() {
            var ch = _port.ReceiveByte();
            if (ch != (int)StkV1Response.InSync) throw new StkNoSyncException();
        }

        private void AssertOk() {
            var ch = _port.ReceiveByte();
            if (ch != (int)StkV1Response.Ok) throw new StkFailedException();
        }

        private void WriteCrcEop() {
            _port.SendByte(CRC_EOP);
        }

    }
}
