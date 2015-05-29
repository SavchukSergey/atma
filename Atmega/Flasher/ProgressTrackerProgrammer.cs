﻿using System;
using System.Linq;
using System.Threading;

namespace Atmega.Flasher {
    public class ProgressTrackerProgrammer : IProgrammer {

        private readonly IProgrammer _inner;
        private readonly DeviceOperation _progressData;
        private readonly CancellationToken _cancellationToken;
        private const int BLOCK_SIZE = 256;

        public ProgressTrackerProgrammer(IProgrammer inner, DeviceOperation progress, CancellationToken cancellationToken) {
            _inner = inner;
            _progressData = progress;
            _cancellationToken = cancellationToken;
        }

        public DeviceOperation ProgressData {
            get { return _progressData; }
        }

        public void Dispose() {
            _inner.Dispose();
        }

        public void Start() {
            _progressData.CurrentState = "Entering programming mode";
            _inner.Start();
        }

        public void Stop() {
            _progressData.CurrentState = "Leaving programming mode";
            _inner.Stop();
        }

        public byte[] ReadPage(int start, int length, AvrMemoryType memType) {
            var offset = start;
            var end = start + length;
            var result = new byte[length];
            while (offset < end) {
                var cnt = Math.Min(end - offset, BLOCK_SIZE);
                _progressData.CurrentState = string.Format("Reading {0} memory {1}-{2}", memType, offset, offset + cnt - 1);
                var page = _inner.ReadPage(offset, cnt, memType);
                foreach (var bt in page) {
                    result[offset - start] = bt;
                    offset++;
                }

                _progressData.IncrementDone(cnt, memType);
                if (_cancellationToken.IsCancellationRequested) {
                    _progressData.CurrentState = "Operation is cancelled";
                }
                _cancellationToken.ThrowIfCancellationRequested();
            }

            return result;
        }

        public void WritePage(int start, AvrMemoryType memType, byte[] data) {
            var offset = start;
            var end = start + data.Length;
            while (offset < end) {
                var cnt = Math.Min(end - offset, BLOCK_SIZE);
                _progressData.CurrentState = string.Format("Writing {0} memory {1}-{2}", memType, offset, offset + cnt - 1);
                _inner.WritePage(offset, memType, data.Skip(offset - start).Take(cnt).ToArray());
                offset += cnt;

                _progressData.IncrementDone(cnt, memType);
                if (_cancellationToken.IsCancellationRequested) {
                    _progressData.CurrentState = "Operation is cancelled";
                }
                _cancellationToken.ThrowIfCancellationRequested();
            }
        }

        public AtmegaLockBits ReadLockBits() {
            _progressData.CurrentState = "Reading lock bits";
            return _inner.ReadLockBits();
        }

        public void WriteLockBits(AtmegaLockBits bits) {
            _progressData.CurrentState = "Writing lock bits";
            _inner.WriteLockBits(bits);
        }
    }
}
