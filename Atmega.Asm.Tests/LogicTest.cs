using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class LogicTest : BaseTestFixture {

        [Test]
        [TestCase("clr r0", (ushort)0x2400)]
        [TestCase("clr r31", (ushort)0x27ff)]
        public void ClrTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("clr r32")]
        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }
    }
}
