using Atmega.Asm.IO;

namespace Atmega.Asm.Tests {
    public abstract class BaseTestFixture {

        protected AsmContext Compile(string content) {
            return new Assembler(new MemoryAsmSource(content)).Load("main.asm");
        }
    }
}
