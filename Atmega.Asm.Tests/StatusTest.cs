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

        [Test]
        [TestCase("clc", (ushort)0x9488)]
        [TestCase("clz", (ushort)0x9498)]
        [TestCase("cln", (ushort)0x94a8)]
        [TestCase("clv", (ushort)0x94b8)]
        [TestCase("cls", (ushort)0x94c8)]
        [TestCase("clh", (ushort)0x94d8)]
        [TestCase("clt", (ushort)0x94e8)]
        [TestCase("cli", (ushort)0x94f8)]
        [TestCase("sec", (ushort)0x9408)]
        [TestCase("sez", (ushort)0x9418)]
        [TestCase("sen", (ushort)0x9428)]
        [TestCase("sev", (ushort)0x9438)]
        [TestCase("ses", (ushort)0x9448)]
        [TestCase("seh", (ushort)0x9458)]
        [TestCase("set", (ushort)0x9468)]
        [TestCase("sei", (ushort)0x9478)]
        public void SimpleInstructionTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

    }
}
