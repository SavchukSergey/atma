using System;
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
            _inner.Start();
        }

        public void Stop() {
            _inner.Stop();
        }

        public byte[] ReadPage(int start, int length, AvrMemoryType memType) {
            var offset = start;
            var end = start + length;
            var result = new byte[length];
            while (offset < end) {
                var cnt = Math.Min(end - offset, BLOCK_SIZE);
                var page = _inner.ReadPage(offset, cnt, memType);
                foreach (var bt in page) {
                    result[offset - start] = bt;
                    offset++;
                }

                _progressData.IncrementDone(cnt, memType);
                _cancellationToken.ThrowIfCancellationRequested();
            }

            return result;

        }

        public void WritePage(int start, AvrMemoryType memType, byte[] data) {
            throw new NotImplementedException();
        }
    }
}
