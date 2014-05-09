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
        [TestCase("ser r16", (ushort)0xef0f)]
        [TestCase("ser r31", (ushort)0xefff)]
        public void SerTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("tst r0", (ushort)0x2000)]
        [TestCase("tst r31", (ushort)0x23ff)]
        public void TstTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("and r31, r1", (ushort)0x21f1)]
        [TestCase("and r1, r31", (ushort)0x221f)]
        public void AndTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("or r31, r0", (ushort)0x29f0)]
        [TestCase("or r0, r31", (ushort)0x2a0f)]
        public void OrTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("eor r31, r1", (ushort)0x25f1)]
        [TestCase("eor r1, r31", (ushort)0x261f)]
        public void EorTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("andi r16, 0", (ushort)0x7000)]
        [TestCase("andi r16, 255", (ushort)0x7f0f)]
        [TestCase("andi r31, 0", (ushort)0x70f0)]
        [TestCase("andi r31, 255", (ushort)0x7fff)]
        public void Reg16Imm8OpcodeTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("ori r16, 0", (ushort)0x6000)]
        [TestCase("ori r16, 255", (ushort)0x6f0f)]
        [TestCase("ori r31, 0", (ushort)0x60f0)]
        [TestCase("ori r31, 255", (ushort)0x6fff)]
        public void OriTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("clr r32")]
        [TestCase("ser r15")]
        [TestCase("ser r32")]
        [TestCase("tst r32")]
        [TestCase("and r0, r32")]
        [TestCase("and r32, r0")]
        [TestCase("or r0, r32")]
        [TestCase("or r32, r0")]
        [TestCase("eor r0, r32")]
        [TestCase("eor r32, r0")]
        [TestCase("andi r8, 0")]
        [TestCase("andi r24, 1234")]
        [TestCase("ori r8, 0")]
        [TestCase("ori r24, 1234")]
        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }
    }
}
