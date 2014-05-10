using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class AssemblerTest : BaseTestFixture {

        [Test]
        public void ArithmeticsTest() {
            var content = LoadEmbeded("arithmetics.asm");
            var compiled = Compile(content);
        }

        [Test]
        public void LogicTest() {
            var content = LoadEmbeded("logic.asm");
            var compiled = Compile(content);
        }

        [Test]
        public void BitTest() {
            var content = LoadEmbeded("bit.asm");
            var compiled = Compile(content);
        }

        [Test]
        public void MoveTest() {
            var content = LoadEmbeded("move.asm");
            var compiled = Compile(content);
        }

        [Test]
        public void BranchTest() {
            var content = LoadEmbeded("branch.asm");
            var compiled = Compile(content);
        }

        [Test]
        public void FlashTest() {
            var complied = CompileEmbedded("flash.asm");
        }

        [Test]
        public void IllegalInstructionTest() {
            try {
                Compile("illegalOpcode");
                Assert.Fail();
            } catch (TokenException) {
            }
        }

        [Test]
        public void AlignCodeTest() {
            const string asm = @"
db 3
nop
dw 0x1234
nop
";
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { 0x0003, 0x0000, 0x1234, 0x0000 }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("break", (ushort)0x9598)]
        [TestCase("nop", (ushort)0x0000)]
        [TestCase("sleep", (ushort)0x9588)]
        [TestCase("wdr", (ushort)0x95a8)]
        public void SimpleInstructionTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }
    }
}
