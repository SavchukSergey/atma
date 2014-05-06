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
        [TestCase("cbr r16, 255", (ushort)0x7000)]
        [TestCase("cbr r16, 0", (ushort)0x7f0f)]
        [TestCase("cbr r31, 255", (ushort)0x70f0)]
        [TestCase("cbr r31, 0", (ushort)0x7fff)]
        public void Reg16ComplementImm8OpcodeTest(string asm, ushort opcode) {
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
        [TestCase("com r0", (ushort)0x9400)]
        [TestCase("com r31", (ushort)0x95f0)]
        public void ComTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("cp r31, r0", (ushort)0x15f0)]
        [TestCase("cp r0, r31", (ushort)0x160f)]
        public void CpTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("cpc r31, r0", (ushort)0x05f0)]
        [TestCase("cpc r0, r31", (ushort)0x060f)]
        public void CpcTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("cpi r16, 0", (ushort)0x3000)]
        [TestCase("cpi r16, 255", (ushort)0x3f0f)]
        [TestCase("cpi r31, 0", (ushort)0x30f0)]
        [TestCase("cpi r31, 255", (ushort)0x3fff)]
        public void CpiTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("add r0, r32")]
        [TestCase("add r32, r0")]
        [TestCase("adc r0, r32")]
        [TestCase("adc r32, r0")]
        [TestCase("asr r32")]
        [TestCase("adiw r24, 64")]
        [TestCase("sbiw r24, 64")]
        [TestCase("adiw r25, 0")]
        [TestCase("sbiw r25, 0")]
        [TestCase("com r32")]
        [TestCase("cbr r0, 0")]
        [TestCase("cbr r32, 0")]
        [TestCase("cp r0, r32")]
        [TestCase("cp r32, r0")]
        [TestCase("cpc r0, r32")]
        [TestCase("cpc r32, r0")]
        [TestCase("cpi r0, 0")]
        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }

    }
}
