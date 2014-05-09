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
        [TestCase("lpm", (ushort)0x95c8)]
        [TestCase("lpm r0, Z", (ushort)0x9004)]
        [TestCase("lpm r31, Z", (ushort)0x91f4)]
        [TestCase("lpm r0, Z+", (ushort)0x9005)]
        [TestCase("lpm r31, Z+", (ushort)0x91f5)]
        public void LpmTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("spm", (ushort)0x95e8)]
        [TestCase("spm z+", (ushort)0x95f8)]
        public void SpmTest(string asm, ushort opcode) {
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
        [TestCase("out 0, r0", (ushort)0xb800)]
        [TestCase("out 63, r0", (ushort)0xbe0f)]
        [TestCase("out 0, r31", (ushort)0xb9f0)]
        [TestCase("out 63, r31", (ushort)0xbfff)]
        public void OutTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("pop r0", (ushort)0x900f)]
        [TestCase("pop r31", (ushort)0x91ff)]
        public void PopTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("push r0", (ushort)0x920f)]
        [TestCase("push r31", (ushort)0x93ff)]
        public void PushTest(string asm, ushort opcode) {
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
        [TestCase("ld r0, x", (ushort)0x900c)]
        [TestCase("ld r0, x+", (ushort)0x900d)]
        [TestCase("ld r0, -x", (ushort)0x900e)]
        [TestCase("ld r31, x", (ushort)0x91fc)]
        [TestCase("ld r31, x+", (ushort)0x91fd)]
        [TestCase("ld r31, -x", (ushort)0x91fe)]
        [TestCase("ld r0, y", (ushort)0x8008)]
        [TestCase("ld r0, y+", (ushort)0x9009)]
        [TestCase("ld r0, -y", (ushort)0x900a)]
        [TestCase("ld r31, y", (ushort)0x81f8)]
        [TestCase("ld r31, y+", (ushort)0x91f9)]
        [TestCase("ld r31, -y", (ushort)0x91fa)]
        [TestCase("ld r0, z", (ushort)0x8000)]
        [TestCase("ld r0, z+", (ushort)0x9001)]
        [TestCase("ld r0, -z", (ushort)0x9002)]
        [TestCase("ld r31, z", (ushort)0x81f0)]
        [TestCase("ld r31, z+", (ushort)0x91f1)]
        [TestCase("ld r31, -z", (ushort)0x91f2)]
        public void LdTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("st x, r0", (ushort)0x920c)]
        [TestCase("st x+, r0", (ushort)0x920d)]
        [TestCase("st -x, r0", (ushort)0x920e)]
        [TestCase("st x, r31", (ushort)0x93fc)]
        [TestCase("st x+, r31", (ushort)0x93fd)]
        [TestCase("st -x, r31", (ushort)0x93fe)]
        [TestCase("st y, r0", (ushort)0x8208)]
        [TestCase("st y+, r0", (ushort)0x9209)]
        [TestCase("st -y, r0", (ushort)0x920a)]
        [TestCase("st y, r31", (ushort)0x83f8)]
        [TestCase("st y+, r31", (ushort)0x93f9)]
        [TestCase("st -y, r31", (ushort)0x93fa)]
        [TestCase("st z, r0", (ushort)0x8200)]
        [TestCase("st z+, r0", (ushort)0x9201)]
        [TestCase("st -z, r0", (ushort)0x9202)]
        [TestCase("st z, r31", (ushort)0x83f0)]
        [TestCase("st z+, r31", (ushort)0x93f1)]
        [TestCase("st -z, r31", (ushort)0x93f2)]
        public void StTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("ldd r0, y", (ushort)0x8008)]
        [TestCase("ldd r0, y+1", (ushort)0x8009)]
        [TestCase("ldd r0, y+63", (ushort)0xac0f)]
        [TestCase("ldd r31, y+63", (ushort)0xadff)]
        [TestCase("ldd r0, z", (ushort)0x8000)]
        [TestCase("ldd r0, z+1", (ushort)0x8001)]
        [TestCase("ldd r0, z+63", (ushort)0xac07)]
        [TestCase("ldd r31, z+63", (ushort)0xadf7)]
        public void LddTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("std y, r0", (ushort)0x8208)]
        [TestCase("std y+1, r0", (ushort)0x8209)]
        [TestCase("std y+63, r0", (ushort)0xae0f)]
        [TestCase("std y+63, r31", (ushort)0xafff)]
        [TestCase("std z, r0", (ushort)0x8200)]
        [TestCase("std z+1, r0", (ushort)0x8201)]
        [TestCase("std z+63, r0", (ushort)0xae07)]
        [TestCase("std z+63, r31", (ushort)0xaff7)]
        public void StdTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("ldi r16, 0", (ushort)0xe000)]
        [TestCase("ldi r16, 255", (ushort)0xef0f)]
        [TestCase("ldi r31, 0", (ushort)0xe0f0)]
        [TestCase("ldi r31, 255", (ushort)0xefff)]
        public void LdiTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("lds r0, $", (ushort)0x9000, (ushort)0x0000)]
        [TestCase("lds r0, 0", (ushort)0x9000, (ushort)0x0000)]
        [TestCase("lds r0, 0xffff", (ushort)0x9000, (ushort)0xffff)]
        [TestCase("lds r31, 0", (ushort)0x91f0, (ushort)0x0000)]
        [TestCase("lds r31, 0xffff", (ushort)0x91f0, (ushort)0xffff)]
        public void LdsTest(string asm, ushort opcode, ushort address) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode, address }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("sts $, r0", (ushort)0x9200, (ushort)0x0000)]
        [TestCase("sts 0, r0", (ushort)0x9200, (ushort)0x0000)]
        [TestCase("sts 0xffff, r0", (ushort)0x9200, (ushort)0xffff)]
        [TestCase("sts 0, r31", (ushort)0x93f0, (ushort)0x0000)]
        [TestCase("sts 0xffff, r31", (ushort)0x93f0, (ushort)0xffff)]
        public void StsTest(string asm, ushort opcode, ushort address) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode, address }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("mov r31, r0", (ushort)0x2df0)]
        [TestCase("mov r0, r31", (ushort)0x2e0f)]
        public void MovTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("movw r1:r0, r31:r30", (ushort)0x010f)]
        [TestCase("movw r31:r30, r1:r0", (ushort)0x01f0)]
        public void MovwTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
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
        
        [TestCase("lpm r32, z")]
        [TestCase("lpm r32, z+")]
        [TestCase("lpm r0, z-")]
        [TestCase("lpm r0, y")]
        [TestCase("lpm r0, y-")]
        [TestCase("lpm r0, y+")]
        [TestCase("lpm r0, x")]
        [TestCase("lpm r0, x-")]
        [TestCase("lpm r0, x+")]
        
        [TestCase("spm x")]
        [TestCase("spm x+")]
        [TestCase("spm x-")]
        [TestCase("spm y")]
        [TestCase("spm y+")]
        [TestCase("spm y-")]
        [TestCase("spm z")]
        [TestCase("spm z-")]

        [TestCase("push r32")]
        [TestCase("pop r32")]

        [TestCase("in r0, 64")]
        [TestCase("in r32, 0")]
        [TestCase("out 64, r0")]
        [TestCase("out 0, r32")]
        
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
        
        [TestCase("ld r32, x")]
        [TestCase("ld r0, -x+")]
        [TestCase("ld r32, y")]
        [TestCase("ld r0, -y+")]
        [TestCase("st x, r32")]
        [TestCase("st -x+, r0")]
        [TestCase("st y, r32")]
        [TestCase("st -y+, r0")]

        [TestCase("ldd r32, y+0")]
        [TestCase("ldd r0, y+64")]
        [TestCase("ldd r32, z+0")]
        [TestCase("ldd r0, z+64")]
        [TestCase("std y+0, r32")]
        [TestCase("std y+64, r0")]
        [TestCase("std z+0, r32")]
        [TestCase("std z+64, r0")]

        [TestCase("ldi r15, 0")]
        [TestCase("ldi r15, 256")]
        [TestCase("lds r0, 0x10000")]
        [TestCase("lds r32, 0")]
        [TestCase("sts 0x10000, r0")]
        [TestCase("sts 0, r32")]

        [TestCase("mov r0, r32")]
        [TestCase("mov r32, r0")]
        
        [TestCase("movw r2:r1, r1:r0")]
        [TestCase("movw r3:r1, r1:r0")]
        [TestCase("movw r1:r0, r2:r1")]
        [TestCase("movw r1:r0, r3:r1")]

        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }
    }
}
