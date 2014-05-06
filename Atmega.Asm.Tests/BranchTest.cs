using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class BranchTest : BaseTestFixture {

        [Test]
        [TestCase("call 0x1234 * 2", (ushort)0x940e, (ushort)0x1234)]
        [TestCase("call ((1 << 22) - 1) * 2", (ushort)0x95ff, (ushort)0xffff)]
        public void CallTest(string asm, ushort opcode, ushort adr) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode, adr }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("brbc 0, ($+2)+0", (ushort)0xf400)]
        [TestCase("brbc 7, ($+2)+0", (ushort)0xf407)]
        [TestCase("brbc 0, ($+2)+63*2", (ushort)0xf5f8)]
        [TestCase("brbc 0, ($+2)-64*2", (ushort)0xf600)]
        public void BrbcTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("brbs 0, ($+2)+0", (ushort)0xf000)]
        [TestCase("brbs 7, ($+2)+0", (ushort)0xf007)]
        [TestCase("brbs 0, ($+2)+63*2", (ushort)0xf1f8)]
        [TestCase("brbs 0, ($+2)-64*2", (ushort)0xf200)]
        public void BrbsTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("brsh $+2", (ushort)0xf400)]
        [TestCase("brlo $+2", (ushort)0xf000)]
        [TestCase("brcc $+2", (ushort)0xf400)]
        [TestCase("brcs $+2", (ushort)0xf000)]
        [TestCase("breq $+2", (ushort)0xf001)]
        [TestCase("brne $+2", (ushort)0xf401)]
        [TestCase("brpl $+2", (ushort)0xf402)]
        [TestCase("brmi $+2", (ushort)0xf002)]
        [TestCase("brvc $+2", (ushort)0xf403)]
        [TestCase("brvs $+2", (ushort)0xf003)]
        [TestCase("brge $+2", (ushort)0xf404)]
        [TestCase("brlt $+2", (ushort)0xf004)]
        [TestCase("brhc $+2", (ushort)0xf405)]
        [TestCase("brhs $+2", (ushort)0xf005)]
        [TestCase("brtc $+2", (ushort)0xf406)]
        [TestCase("brts $+2", (ushort)0xf006)]
        [TestCase("brie $+2", (ushort)0xf007)]
        [TestCase("brid $+2", (ushort)0xf407)]
        public void StatusBranchTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }


        [Test]
        [TestCase("brbc 0, ($+2)+64*2")]
        [TestCase("brbc 0, ($+2)-65*2")]
        [TestCase("brbc 8, ($+2)+0*2")]
        [TestCase("brbs 0, ($+2)+64*2")]
        [TestCase("brbs 0, ($+2)-65*2")]
        [TestCase("brbs 8, ($+2)+0*2")]
        [TestCase("brcc ($+2)+64*2")]
        [TestCase("brcc ($+2)-65*2")]
        [TestCase("brcs ($+2)+64*2")]
        [TestCase("brcs ($+2)-65*2")]
        [TestCase("breq ($+2)+64*2")]
        [TestCase("breq ($+2)-65*2")]
        [TestCase("brge ($+2)+64*2")]
        [TestCase("brge ($+2)-65*2")]
        [TestCase("brhc ($+2)+64*2")]
        [TestCase("brhc ($+2)-65*2")]
        [TestCase("brhs ($+2)+64*2")]
        [TestCase("brhs ($+2)-65*2")]
        [TestCase("brid ($+2)+64*2")]
        [TestCase("brid ($+2)-65*2")]
        [TestCase("brie ($+2)+64*2")]
        [TestCase("brie ($+2)-65*2")]
        [TestCase("brlo ($+2)+64*2")]
        [TestCase("brlo ($+2)-65*2")]
        [TestCase("brlt ($+2)+64*2")]
        [TestCase("brlt ($+2)-65*2")]
        [TestCase("brmi ($+2)+64*2")]
        [TestCase("brmi ($+2)-65*2")]
        [TestCase("brne ($+2)+64*2")]
        [TestCase("brne ($+2)-65*2")]
        [TestCase("brpl ($+2)+64*2")]
        [TestCase("brpl ($+2)-65*2")]
        [TestCase("brsh ($+2)+64*2")]
        [TestCase("brsh ($+2)-65*2")]
        [TestCase("brtc ($+2)+64*2")]
        [TestCase("brtc ($+2)-65*2")]
        [TestCase("brts ($+2)+64*2")]
        [TestCase("brts ($+2)-65*2")]
        [TestCase("brvc ($+2)+64*2")]
        [TestCase("brvc ($+2)-65*2")]
        [TestCase("brvs ($+2)+64*2")]
        [TestCase("brvs ($+2)-65*2")]
        [TestCase("call ((1 << 22) - 0) * 2")]
        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }

    }
}
