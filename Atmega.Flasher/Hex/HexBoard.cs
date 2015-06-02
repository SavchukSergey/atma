using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Atmega.Hex;

namespace Atmega.Flasher.Hex {
    public class HexBoard : INotifyPropertyChanged {

        private readonly ObservableCollection<HexBoardLine> _lines = new ObservableCollection<HexBoardLine>();

        private HexBoardLine EnsureLine(int adr) {
            adr = (adr >> 4) << 4;
            var line = _lines.FirstOrDefault(item => item.Address == adr);
            if (line == null) {
                var i = 0;
                while (i < _lines.Count && _lines[i].Address < adr)
                    i++;

                line = new HexBoardLine {
                    Address = adr
                };
                _lines.Insert(i, line);
            }
            return line;
        }

        public byte? this[int address] {
            get { return null; }
            set {
                var line = EnsureLine(address);
                line.Bytes[address % 16] = value;
                OnPropertyChanged("HexFormatted");
            }
        }

        public ObservableCollection<HexBoardLine> Lines { get { return _lines; } }

        public static HexBoard From(HexFile file) {
            var res = new HexBoard();
            foreach (var line in file.Lines) {
                for (int index = 0; index < line.Data.Length; index++) {
                    var bt = line.Data[index];
                    res[line.Address + index] = bt;
                }
            }
            return res;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public static HexBoard From(byte[] data) {
            var board = new HexBoard();

            var offset = 0;

            foreach (var bt in data) {
                board[offset] = bt;
                offset++;
            }

            return board;
        }

        public HexBlocks SplitBlocks(int pageSize = 1) {
            var res = new HexBlocks();
            var blockStart = 0;
            var address = 0;
            var bytes = new List<byte>();

            var allBytes = Lines
                .SelectMany(l => l.Bytes.Select((b, i) => new HexBlockByte { Address = l.Address + i, Byte = b.Value }))
                .Where(item => item.Byte.HasValue);
            foreach (var bt in allBytes) {
                if (bt.Address != address && (bt.Address / pageSize == address / pageSize)) {
                    while (address != bt.Address) {
                        bytes.Add(0xff);
                        address++;
                    }
                }

                if (bt.Address != address) {
                    if (bytes.Count > 0) {
                        res.Blocks.Add(new HexBlock {
                            Address = blockStart,
                            Data = bytes.ToArray()
                        });
                        bytes = new List<byte>();
                    }
                    blockStart = bt.Address;
                    address = blockStart;
                }
                if (bt.Byte.HasValue) bytes.Add(bt.Byte.Value);
                address++;
            }
            if (bytes.Count != 0) {
                res.Blocks.Add(new HexBlock {
                    Address = blockStart,
                    Data = bytes.ToArray()
                });
            }
            return res;
        }

        public void Clear() {
            Lines.Clear();
        }
    }
}
