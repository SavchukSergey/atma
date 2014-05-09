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
        [TestCase("bst r0, 0", (ushort)0xfa00)]
        [TestCase("bst r0, 7", (ushort)0xfa07)]
        [TestCase("bst r31, 0", (ushort)0xfbf0)]
        [TestCase("bst r31, 7", (ushort)0xfbf7)]
        public void BstTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("cbi 0, 0", (ushort)0x9800)]
        [TestCase("cbi 0, 7", (ushort)0x9807)]
        [TestCase("cbi 31, 0", (ushort)0x98f8)]
        [TestCase("cbi 31, 7", (ushort)0x98ff)]
        public void CbiTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("sbi 0, 0", (ushort)0x9a00)]
        [TestCase("sbi 0, 7", (ushort)0x9a07)]
        [TestCase("sbi 31, 0", (ushort)0x9af8)]
        [TestCase("sbi 31, 7", (ushort)0x9aff)]
        public void SbiTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("lsl r0", (ushort)0x0c00)]
        [TestCase("lsl r31", (ushort)0x0fff)]
        public void LslTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("lsr r0", (ushort)0x9406)]
        [TestCase("lsr r31", (ushort)0x95f6)]
        public void LsrTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("rol r0", (ushort)0x1c00)]
        [TestCase("rol r31", (ushort)0x1fff)]
        public void RolTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("ror r0", (ushort)0x9407)]
        [TestCase("ror r31", (ushort)0x95f7)]
        public void RorTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("bld r32, 0")]
        [TestCase("bld r0, 8")]
        [TestCase("bst r32, 0")]
        [TestCase("bst r0, 8")]
        [TestCase("cbi 32, 0")]
        [TestCase("cbi 0, 8")]
        [TestCase("sbi 32, 0")]
        [TestCase("sbi 0, 8")]
        [TestCase("lsl r32")]
        [TestCase("lsr r32")]
        [TestCase("rol r32")]
        [TestCase("ror r32")]
        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }

    }
}
