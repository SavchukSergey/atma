namespace Atmega.Asm.IO {
    public interface IAsmSource {
        string LoadContent(string fileName);
        bool FileExists(string fileName);
        string ResolveFile(string fileName, string referrer);
    }
}
