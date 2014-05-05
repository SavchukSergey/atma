using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class ArithmeticsTest : BaseTestFixture {

        [Test]
        [TestCase("adc r31, r1", (ushort)0x1df1)]
        [TestCase("adc r1, r31", (ushort)0x1e1f)]
        public void AdcTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("add r31, r1", (ushort)0x0df1)]
        [TestCase("add r1, r31", (ushort)0x0e1f)]
        public void AddTest(string asm, ushort opcode) {
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
        [TestCase("andi r16, 0", (ushort)0x7000)]
        [TestCase("andi r16, 255", (ushort)0x7f0f)]
        [TestCase("andi r31, 0", (ushort)0x70f0)]
        [TestCase("andi r31, 255", (ushort)0x7fff)]
        public void Reg16Imm8OpcodeTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("asr r0", (ushort)0x9405)]
        [TestCase("asr r16", (ushort)0x9505)]
        [TestCase("asr r31", (ushort)0x95f5)]
        public void AsrTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("adiw r24, 0", (ushort)0x9600)]
        [TestCase("adiw r24, 63", (ushort)0x96cf)]
        [TestCase("adiw r30, 63", (ushort)0x96ff)]
        [TestCase("sbiw r24, 0", (ushort)0x9700)]
        [TestCase("sbiw r24, 63", (ushort)0x97cf)]
        [TestCase("sbiw r30, 63", (ushort)0x97ff)]
        public void RegWImm6BoundariesTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("add r0, r32")]
        [TestCase("add r32, r0")]
        [TestCase("adc r0, r32")]
        [TestCase("adc r32, r0")]
        [TestCase("and r0, r32")]
        [TestCase("and r32, r0")]
        [TestCase("asr r32")]
        [TestCase("adiw r24, 64")]
        [TestCase("sbiw r24, 64")]
        [TestCase("adiw r25, 0")]
        [TestCase("sbiw r25, 0")]
        [TestCase("andi r8, 0")]
        [TestCase("andi r24, 1234")]
        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }

    }
}
