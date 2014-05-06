using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class MovesTest : BaseTestFixture {

        [Test]
        [TestCase("elpm", (ushort)0x95d8)]
        [TestCase("elpm r0, Z", (ushort)0x9006)]
        [TestCase("elpm r31, Z", (ushort)0x91f6)]
        [TestCase("elpm r0, Z+", (ushort)0x9007)]
        [TestCase("elpm r31, Z+", (ushort)0x91f7)]
        public void ElpmTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        public void Reg32Imm16Opcode() {
            var complied = Compile("lds r31, $");
            Assert.AreEqual(4, complied.CodeSection.Content.Count);
            try {
                Compile("lds rn, $");
                Assert.Fail();
            } catch (TokenException) {
            }
        }

        [Test]
        public void Imm16Reg32Opcode() {
            var complied = Compile("sts $, r31");
            Assert.AreEqual(4, complied.CodeSection.Content.Count);
            try {
                Compile("sts $, rn");
                Assert.Fail();
            } catch (TokenException) {
            }
        }

        [Test]
        [TestCase("elpm r32, z")]
        [TestCase("elpm r32, z+")]
        [TestCase("elpm r0, z-")]
        [TestCase("elpm r0, y")]
        [TestCase("elpm r0, y-")]
        [TestCase("elpm r0, y+")]
        [TestCase("elpm r0, x")]
        [TestCase("elpm r0, x-")]
        [TestCase("elpm r0, x+")]
        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }
    }
}
