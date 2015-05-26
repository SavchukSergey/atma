﻿using System;
using System.Threading;

namespace Atmega.Flasher.AvrSpi {
    public class AvrSpiClient : IDisposable {

        private readonly SpiMaster _spiMaster;

        public AvrSpiClient(SpiMaster spiMaster) {
            _spiMaster = spiMaster;
        }

        public void Open() {
            _spiMaster.Open();
        }

        public void Close() {
            _spiMaster.Close();
        }

        public void StartProgram() {
            ResetPin = true;
            _spiMaster.ResetClock();
            Thread.Sleep(50);
            ResetPin = false;
            Thread.Sleep(50);
            ProgrammingEnable();
        }

        public void EndProgram() {

        }

        public void Stop() {
            ResetPin = true;
        }

        public byte SpiTransaction(byte a, byte b, byte c, byte d) {
            _spiMaster.SendByte(a);
            _spiMaster.SendByte(b);
            _spiMaster.SendByte(c);
            return _spiMaster.SendByte(d);
        }

        public byte ProgrammingEnable() {
            return SpiTransaction(0xAC, 0x53, 0x00, 0x00);
        }

        public void ChipErase() {
            SpiTransaction(0xac, 0x80, 0x00, 0x00);
            Thread.Sleep(10);
            Thread.Sleep(1000);
            ResetPin = true;
            Thread.Sleep(50);
            ResetPin = false;
        }

        public byte PollRdyBusy(byte arg) {
            return SpiTransaction(0xf0, 0x00, 0x00, arg);
        }

        #region Load Instructions

        public void LoadExtendedAddressByte(byte val) {
            SpiTransaction(0x4d, 0x00, val, 0x00);
        }

        public void LoadProgramMemoryPageHighByte(ushort address, byte highByte) {
            SpiTransaction(0x48, (byte)((address >> 8) & 0xff), (byte)(address & 0xff), highByte);
        }

        public void LoadProgramMemoryPageLowByte(ushort address, byte lowByte) {
            SpiTransaction(0x40, (byte)((address >> 8) & 0xff), (byte)(address & 0xff), lowByte);
        }

        public void LoadEepromMemoryPage(byte adr, byte val) {
            SpiTransaction(0xc1, 0x00, (byte)(adr & 0x03), val);
        }

        #endregion

        #region Read Instructions

        public byte ReadProgramMemoryPageHighByte(ushort address) {
            return SpiTransaction(0x28, (byte)((address >> 8) & 0xff), (byte)(address & 0xff), 0);
        }

        public byte ReadProgramMemoryPageLowByte(ushort address) {
            return SpiTransaction(0x20, (byte)((address >> 8) & 0xff), (byte)(address & 0xff), 0);
        }

        public byte ReadEepromMemory(ushort address) {
            address &= 0x3ff;
            return SpiTransaction(0xa0, (byte)((address >> 8) & 0xff), (byte)(address & 0xff), 0xff);
        }

        public byte ReadLockBits() {
            return SpiTransaction(0x58, 0x00, 0x00, 0x00);
        }

        public byte ReadSignatureByte(byte adr) {
            return SpiTransaction(0x30, 0x00, (byte)(adr & 0x03), 0x00);
        }

        public byte ReadFuseBits() {
            return SpiTransaction(0x50, 0x00, 0x00, 0x00);
        }

        public byte ReadFuseHighBits() {
            return SpiTransaction(0x58, 0x08, 0x00, 0x00);
        }

        public byte ReadExtendedHighBits() {
            return SpiTransaction(0x50, 0x08, 0x00, 0x00);
        }

        public byte ReadCalibrationByte() {
            return SpiTransaction(0x38, 0x00, 0x00, 0x00);
        }

        #endregion

        #region Write Instructions

        public void WriteProgramMemoryPage(ushort address) {
            SpiTransaction(0x4c, (byte)((address >> 8) & 0xff), (byte)(address & 0xff), 0);
        }

        public void WriteEepromMemory(ushort address, byte val) {
            address &= 0x3ff;
            SpiTransaction(0xc0, (byte)((address >> 8) & 0xff), (byte)(address & 0xff), val);
            Thread.Sleep(4);
        }

        public void WriteEepromMemoryPage(byte adr, byte val) {
            var adrs = adr << 2;
            SpiTransaction(0xc2, (byte)((adrs >> 8) & 0xff), (byte)(adrs & 0xff), val);
        }

        public byte WriteLockBits(byte val) {
            return SpiTransaction(0xac, 0xe0, 0x00, val);
        }

        public byte WriteFuseBits(byte val) {
            return SpiTransaction(0xac, 0xa0, 0x00, val);
        }

        public byte WriteHighFuseBits(byte val) {
            return SpiTransaction(0xac, 0xa8, 0x00, val);
        }

        public byte WriteExtendedFuseBits(byte val) {
            return SpiTransaction(0xac, 0xa4, 0x00, val);
        }

        #endregion

        public byte ReadFlashByte(ushort address) {
            return (address & 1) == 0 ? ReadProgramMemoryPageLowByte((ushort)(address >> 1)) : ReadProgramMemoryPageHighByte((ushort)(address >> 1));
        }

        public void WriteFlashByte(ushort address, byte val) {
            if ((address & 1) == 0) {
                LoadProgramMemoryPageLowByte((ushort)(address >> 1), val);
            } else {
                LoadProgramMemoryPageHighByte((ushort)(address >> 1), val);
            }
        }


        public AvrSignature GetSignature() {
            return new AvrSignature {
                Vendor = ReadSignatureByte(0),
                Middle = ReadSignatureByte(1),
                Low = ReadSignatureByte(2)
            };
        }


        private bool ResetPin {
            get { return false; }
            set { }
        }

        public void Dispose() {
            _spiMaster.Dispose();
        }

    }
}
