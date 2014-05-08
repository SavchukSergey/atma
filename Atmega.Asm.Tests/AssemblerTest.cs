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
        public void RjmpTest() {
            var compiled = Compile(@"
section code
back:
    nop
    rjmp back
self:
    rjmp self
    rjmp zero
zero:
    rjmp forward
    nop
forward:
");
            Assert.AreEqual(new[] {
                0, 0,
                0xfe, 0xcf,
                0xff, 0xcf,
                0x00, 0xc0,
                0x01, 0xc0,
                0x00, 0x00
            }, compiled.CodeSection.Content);
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
        public void SimpleInstructionTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }
    }
}
