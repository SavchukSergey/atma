using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class AssemblerTest : BaseTestFixture {

        [Test]
        [Ignore]
        public void ArithmeticsTest() {
            var content = LoadEmbeded("arithmetics.asm");
            var compiled = Compile(content);
        }

        [Test]
        [Ignore]
        public void LogicTest() {
            var content = LoadEmbeded("logic.asm");
            var compiled = Compile(content);
        }

        [Test]
        [Ignore]
        public void BitTest() {
            var content = LoadEmbeded("bit.asm");
            var compiled = Compile(content);
        }

        [Test]
        [Ignore]
        public void MoveTest() {
            var content = LoadEmbeded("move.asm");
            var compiled = Compile(content);
        }

        [Test]
        [Ignore]
        public void BranchTest() {
            var content = LoadEmbeded("branch.asm");
            var compiled = Compile(content);
        }

        [Test]
        [Ignore]
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
        [TestCase("break", (ushort)0x9598)]
        [TestCase("nop", (ushort)0x0000)]
        [TestCase("sleep", (ushort)0x9588)]
        public void SimpleInstructionTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }
    }
}
