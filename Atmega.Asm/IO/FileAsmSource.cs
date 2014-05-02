using System.IO;

namespace Atmega.Asm.IO {
    public class FileAsmSource : IAsmSource {

        public virtual string LoadContent(string fileName) {
            using (var reader = new StreamReader(fileName)) {
                return reader.ReadToEnd();
            }
        }

        public virtual bool FileExists(string fileName) {
            return File.Exists(fileName);
        }

        public virtual string ResolveFile(string fileName, string referrer) {
            var basePath = referrer != null ? Path.GetDirectoryName(referrer) : null;
            if (basePath != null) {
                var path = Path.Combine(basePath, fileName);
                if (FileExists(path)) return path;
            }

            return null;
        }

    }
}
