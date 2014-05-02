using System;

namespace Atmega.Asm.IO {
    public class MemoryAsmSource : IAsmSource {

        private readonly string _content;

        public MemoryAsmSource(string content) {
            _content = content;
        }

        public string LoadContent(string fileName) {
            if (fileName == "main.asm") {
                return _content;
            }
            throw new InvalidOperationException();
        }

        public bool FileExists(string fileName) {
            return false;
        }

        public string ResolveFile(string fileName, string referrer) {
            return fileName;
        }
    }
}
