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
        [TestCase("in r0, 0", (ushort)0xb000)]
        [TestCase("in r0, 63", (ushort)0xb60f)]
        [TestCase("in r31, 0", (ushort)0xb1f0)]
        [TestCase("in r31, 63", (ushort)0xb7ff)]
        public void InTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("lac z, r0", (ushort)0x9206)]
        [TestCase("lac z, r31", (ushort)0x93f6)]
        public void LacTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("las z, r0", (ushort)0x9205)]
        [TestCase("las z, r31", (ushort)0x93f5)]
        public void LasTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("lat z, r0", (ushort)0x9207)]
        [TestCase("lat z, r31", (ushort)0x93f7)]
        public void LatTest(string asm, ushort opcode) {
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
        [TestCase("in r0, 64")]
        [TestCase("in r32, 0")]
        
        [TestCase("lac z, r32")]
        [TestCase("lac z+, r0")]
        [TestCase("lac z-, r0")]
        [TestCase("lac y, r0")]
        [TestCase("lac y+, r0")]
        [TestCase("lac y-, r0")]
        [TestCase("lac x, r0")]
        [TestCase("lac x+, r0")]
        [TestCase("lac x-, r0")]

        [TestCase("las z, r32")]
        [TestCase("las z+, r0")]
        [TestCase("las z-, r0")]
        [TestCase("las y, r0")]
        [TestCase("las y+, r0")]
        [TestCase("las y-, r0")]
        [TestCase("las x, r0")]
        [TestCase("las x+, r0")]
        [TestCase("las x-, r0")]

        [TestCase("lat z, r32")]
        [TestCase("lat z+, r0")]
        [TestCase("lat z-, r0")]
        [TestCase("lat y, r0")]
        [TestCase("lat y+, r0")]
        [TestCase("lat y-, r0")]
        [TestCase("lat x, r0")]
        [TestCase("lat x+, r0")]
        [TestCase("lat x-, r0")]

        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }
    }
}
