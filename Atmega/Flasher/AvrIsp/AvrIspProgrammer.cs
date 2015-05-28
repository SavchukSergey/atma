﻿using System;

namespace Atmega.Flasher.AvrIsp {
    public class AvrIspProgrammer : IProgrammer {

        private readonly AvrIspClient _client;
        private const int BLOCK_SIZE = 1024;

        public AvrIspProgrammer(AvrIspClient client) {
            _client = client;
        }

        public void Start() {
            _client.Open();
            _client.ResetDevice();
            _client.StartProgram();
        }

        public void Stop() {
            _client.EndProgram();
            _client.Close();
        }

        public byte[] ReadPage(int start, int length, AvrMemoryType memType) {
            switch (memType) {
                case AvrMemoryType.Eeprom:
                    return ReadEeprom(start, length);
                case AvrMemoryType.Flash:
                    return ReadFlash(start, length);
                default:
                    throw new NotSupportedException();
            }
        }

        public void WritePage(int start, AvrMemoryType memType, byte[] data) {
            throw new System.NotImplementedException();
        }

        private byte[] ReadEeprom(int start, int length) {
            var offset = start;
            var end = start + length;
            var result = new byte[length];
            while (offset < end) {
                _client.SetAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);

                var data = _client.ReadEeprom(cnt);

                foreach (var bt in data) {
                    result[offset - start] = bt;
                    offset++;
                }
            }

            return result;
        }

        private byte[] ReadFlash(int start, int length, Action<DeviceOperation> progressCallbackData = null) {
            var offset = start;
            var end = start + length;
            var result = new byte[length];
            while (offset < end) {
                _client.SetAddress((ushort)(offset >> 1));
                var cnt = Math.Min(end - offset, BLOCK_SIZE);

                var data = _client.ReadFlash(cnt);

                foreach (var bt in data) {
                    result[offset - start] = bt;
                    offset++;
                }
            }

            return result;
        }

        public void Dispose() {
            _client.Dispose();
        }
    }
}