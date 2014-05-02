namespace Atmega.Asm.IO {
    public interface IAsmSource {
        
        string LoadContent(string fileName);
        
        string ResolveFile(string fileName, string referrer);

    }
}
