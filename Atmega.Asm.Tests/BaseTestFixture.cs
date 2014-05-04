using System.IO;
using Atmega.Asm.IO;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Tests {
    public abstract class BaseTestFixture {

        protected AsmContext Compile(string content) {
            return new Assembler(new MemoryAsmSource(content)).Load("main.asm");
        }

        protected AsmContext ComplieEmbeded(string name) {
            var type = GetType();
            var ns = type.Namespace;
            var basePath = ns + ".Samples.";
            return new Assembler(new EmbeddedAsmSource(type.Assembly, basePath)).Load(name);
        }

        protected string LoadEmbeded(string name) {
            var type = GetType();
            var ns = type.Namespace;
            var fullname = ns + ".Samples." + name;
            var stream = type.Assembly.GetManifestResourceStream(fullname);
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        protected TokensQueue Tokenize(string content) {
            return new TokensQueue(new Tokenizer().Read(content));
        }


    }
}
