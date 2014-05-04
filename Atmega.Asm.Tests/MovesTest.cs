using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class MovesTest : BaseTestFixture {


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
    }
}
