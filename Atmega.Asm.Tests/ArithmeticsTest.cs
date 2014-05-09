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
        [TestCase("sbc r31, r0", (ushort)0x09f0)]
        [TestCase("sbc r0, r31", (ushort)0x0a0f)]
        public void SbcTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("sub r31, r1", (ushort)0x19f1)]
        [TestCase("sub r1, r31", (ushort)0x1a1f)]
        public void SubTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("sbci r16, 0", (ushort)0x4000)]
        [TestCase("sbci r16, 255", (ushort)0x4f0f)]
        [TestCase("sbci r31, 0", (ushort)0x40f0)]
        [TestCase("sbci r31, 255", (ushort)0x4fff)]
        public void SbciTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("subi r16, 0", (ushort)0x5000)]
        [TestCase("subi r16, 255", (ushort)0x5f0f)]
        [TestCase("subi r31, 0", (ushort)0x50f0)]
        [TestCase("subi r31, 255", (ushort)0x5fff)]
        public void SubiTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }
        
        [Test]
        [TestCase("cbr r16, 255", (ushort)0x7000)]
        [TestCase("cbr r16, 0", (ushort)0x7f0f)]
        [TestCase("cbr r31, 255", (ushort)0x70f0)]
        [TestCase("cbr r31, 0", (ushort)0x7fff)]
        public void CbrTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("sbr r16, 0", (ushort)0x6000)]
        [TestCase("sbr r16, 255", (ushort)0x6f0f)]
        [TestCase("sbr r31, 0", (ushort)0x60f0)]
        [TestCase("sbr r31, 255", (ushort)0x6fff)]
        public void SbrTest(string asm, ushort opcode) {
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
        [TestCase("adiw r30, 0", (ushort)0x9630)]
        [TestCase("adiw r30, 63", (ushort)0x96ff)]
        public void AdiwTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("sbiw r24, 0", (ushort)0x9700)]
        [TestCase("sbiw r24, 63", (ushort)0x97cf)]
        [TestCase("sbiw r30, 0", (ushort)0x9730)]
        [TestCase("sbiw r30, 63", (ushort)0x97ff)]
        public void SbiwTest(string asm, ushort opcode) {
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
        [TestCase("neg r0", (ushort)0x9401)]
        [TestCase("neg r31", (ushort)0x95f1)]
        public void NegTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("dec r0", (ushort)0x940a)]
        [TestCase("dec r31", (ushort)0x95fa)]
        public void DecTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("inc r0", (ushort)0x9403)]
        [TestCase("inc r31", (ushort)0x95f3)]
        public void IncTest(string asm, ushort opcode) {
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
        [TestCase("fmul r16, r16", (ushort)0x0308)]
        [TestCase("fmul r16, r23", (ushort)0x030f)]
        [TestCase("fmul r23, r16", (ushort)0x0378)]
        [TestCase("fmul r23, r23", (ushort)0x037f)]
        public void FmulTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("fmuls r16, r16", (ushort)0x0380)]
        [TestCase("fmuls r16, r23", (ushort)0x0387)]
        [TestCase("fmuls r23, r16", (ushort)0x03f0)]
        [TestCase("fmuls r23, r23", (ushort)0x03f7)]
        public void FmulsTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("fmulsu r16, r16", (ushort)0x0388)]
        [TestCase("fmulsu r16, r23", (ushort)0x038f)]
        [TestCase("fmulsu r23, r16", (ushort)0x03f8)]
        [TestCase("fmulsu r23, r23", (ushort)0x03ff)]
        public void FmulsuTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("mul r0, r31", (ushort)0x9e0f)]
        [TestCase("mul r31, r0", (ushort)0x9df0)]
        public void MulTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("muls r16, r31", (ushort)0x020f)]
        [TestCase("muls r31, r16", (ushort)0x02f0)]
        public void MulsTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("mulsu r16, r23", (ushort)0x0307)]
        [TestCase("mulsu r23, r16", (ushort)0x0370)]
        public void MulsuTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("des 0", (ushort)0x940b)]
        [TestCase("des 1", (ushort)0x941b)]
        [TestCase("des 15", (ushort)0x94fb)]
        public void DesTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("add r0, r32")]
        [TestCase("add r32, r0")]
        [TestCase("sub r0, r32")]
        [TestCase("sub r32, r0")]
        [TestCase("adc r0, r32")]
        [TestCase("adc r32, r0")]
        [TestCase("sbc r0, r32")]
        [TestCase("sbc r32, r0")]

        [TestCase("sbci r15, 0")]
        [TestCase("sbci r16, 256")]
        [TestCase("subi r15, 0")]
        [TestCase("subi r16, 256")]
        
        [TestCase("asr r32")]
        [TestCase("adiw r24, 64")]
        [TestCase("sbiw r24, 64")]
        [TestCase("adiw r25, 0")]
        [TestCase("sbiw r25, 0")]
        [TestCase("com r32")]
        [TestCase("neg r32")]
        [TestCase("dec r32")]
        [TestCase("inc r32")]

        [TestCase("cbr r15, 0")]
        [TestCase("cbr r16, 256")]
        [TestCase("cbr r32, 0")]
        [TestCase("sbr r15, 0")]
        [TestCase("sbr r16, 256")]
        [TestCase("sbr r32, 0")]

        [TestCase("cp r0, r32")]
        [TestCase("cp r32, r0")]
        [TestCase("cpc r0, r32")]
        [TestCase("cpc r32, r0")]
        [TestCase("cpi r0, 0")]
        [TestCase("des 16")]
        [TestCase("fmul r15, r16")]
        [TestCase("fmul r16, r15")]
        [TestCase("fmul r16, r24")]
        [TestCase("fmul r24, r16")]
        [TestCase("fmuls r15, r16")]
        [TestCase("fmuls r16, r15")]
        [TestCase("fmuls r16, r24")]
        [TestCase("fmuls r24, r16")]
        [TestCase("fmulsu r15, r16")]
        [TestCase("fmulsu r16, r15")]
        [TestCase("fmulsu r16, r24")]
        [TestCase("fmulsu r24, r16")]

        [TestCase("mul r0, r32")]
        [TestCase("mul r32, r0")]

        [TestCase("muls r16, r32")]
        [TestCase("muls r32, r16")]
        [TestCase("muls r16, r15")]
        [TestCase("muls r15, r16")]

        [TestCase("mulsu r16, r24")]
        [TestCase("mulsu r24, r16")]
        [TestCase("mulsu r16, r15")]
        [TestCase("mulsu r15, r16")]
        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }

    }
}
