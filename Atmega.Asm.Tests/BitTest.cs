using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class BitTest : BaseTestFixture {

        [Test]
        [TestCase("bld r0, 0", (ushort)0xf800)]
        [TestCase("bld r0, 7", (ushort)0xf807)]
        [TestCase("bld r31, 0", (ushort)0xf9f0)]
        [TestCase("bld r31, 7", (ushort)0xf9f7)]
        public void BldTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("bld r32, 0")]
        [TestCase("bld r0, 8")]
        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }

    }
}
