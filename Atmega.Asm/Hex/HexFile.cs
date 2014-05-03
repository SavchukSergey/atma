using System;
using System.Collections.Generic;
using System.IO;

namespace Atmega.Asm.Hex {
    public class HexFile {

        private readonly List<HexFileLine> _lines = new List<HexFileLine>();

        public List<HexFileLine> Lines {
            get { return _lines; }
        }

        public byte[] GetCode() {
            var size = CodeSize;
            var code = new byte[size];
            foreach (var line in _lines) {
                if (line.Type == HexFileLineType.Data) {
                    for (var i = 0; i < line.Data.Length; i++) {
                        code[line.Address + i] = line.Data[i];
                    }
                }
            }

            return code;
        }

        public int CodeSize {
            get {
                var res = 0;
                foreach (var line in _lines) {
                    if (line.Type == HexFileLineType.Data) {
                        res = Math.Max(res, line.Address + line.Data.Length);
                    }
                }
                return res;
            }
        }

        public static HexFile Load(string path) {
            using (var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                return Parse(new StreamReader(file));
            }
        }

        public static HexFile Parse(TextReader reader) {
            var res = new HexFile();
            var lineNumber = 1;
            try {
                while (reader.Peek() != -1) {
                    var rawLine = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(rawLine)) {
                        var line = HexFileLine.Parse(rawLine);
                        res.Lines.Add(line);
                    }
                    lineNumber++;
                }
            } catch (Exception e) {
                throw new Exception("couldn't parse line " + lineNumber + ". " + e.Message);
            }
            return res;
        }
    }
}
