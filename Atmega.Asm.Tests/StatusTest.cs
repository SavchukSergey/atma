using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class StatusTest : BaseTestFixture {

        [Test]
        [TestCase("bclr 0", (ushort)0x9488)]
        [TestCase("bclr 7", (ushort)0x94f8)]
        public void BclrTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("bset 0", (ushort)0x9408)]
        [TestCase("bset 7", (ushort)0x9478)]
        public void BsetTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("bclr 8")]
        [TestCase("bset 8")]
        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }

    }
}
