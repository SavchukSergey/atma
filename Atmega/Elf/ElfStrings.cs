using System.Collections.Generic;
using System.Text;

namespace Atmega.Elf {
    public class ElfStrings {

        private readonly IDictionary<string, uint> _offsets = new Dictionary<string, uint>();
        private readonly List<byte> _data = new List<byte>();

        public ElfStrings() {
            SaveString(string.Empty);
        }

        public int BytesSize {
            get { return _data.Count; }
        }

        public uint SaveString(string val) {
            uint offset;
            if (_offsets.TryGetValue(val, out offset)) return offset;
            offset = (uint)_data.Count;
            var data = Encoding.ASCII.GetBytes(val);
            _data.AddRange(data);
            _data.Add(0);
            return offset;
        }

        public byte[] ToArray() {
            return _data.ToArray();
        }
    }
}
