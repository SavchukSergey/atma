using System.Collections.Generic;
using System.Linq;
using Atmega.Asm.Hex;

namespace Atmega.Hex {
    public class HexFileBuilder {

        private HexFile _hf = new HexFile();
        private HexFileLine _line;
        private bool _newLine;
        private int _offset;
        private List<byte> _currentData = new List<byte>();

        public void SetAddress(int val) {
            _offset = val;
        }

        public void WriteByte(byte? bt) {
            if (bt.HasValue) WriteByte(bt.Value);
            else {
                _offset++;
                FlushLine();
            }
        }

        public void WriteByte(byte bt) {
            if (!CheckCurrentLine(_offset)) {
                FlushLine();
            }

            if (_line == null) {
                EnsureLine(_offset);
            }
            var col = _offset - _line.Address;
            while (_currentData.Count <= col) _currentData.Add(0xff);
            _currentData[col] = bt;
            _offset++;
        }

        private bool CheckCurrentLine(int adr) {
            if (_line == null) return false;
            var col = adr - _line.Address;
            if (col < 0) return false;
            if (col >= 16) return false;

            if (_currentData.Count < col) return false;
            return true;
        }

        private void EnsureLine(int adr) {
            _line = _hf.Lines.FirstOrDefault(item => item.Type == HexFileLineType.Data && (item.Address >> 4) == (adr >> 4));
            if (_line == null) {
                _line = new HexFileLine {
                    Address = (ushort) _offset,
                    Type = HexFileLineType.Data,
                    Data = new byte[0]
                };
                _newLine = true;
            } else {
                _newLine = false;
            }
            _currentData = new List<byte>(_line.Data);
        }

        private void FlushLine() {
            if (_line != null && _newLine && _currentData.Count > 0) {
                _line.Data = _currentData.ToArray();
                _hf.Lines.Add(_line);
                _line = null;
                _newLine = false;
            }
        }

        public HexFile Build() {
            FlushLine();
            _hf.Lines.Add(new HexFileLine {
                Type = HexFileLineType.Eof
            });
            var res = _hf;
            _hf = new HexFile();
            _offset = 0;
            return res;
        }
    }
}
