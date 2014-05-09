using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class BranchTest : BaseTestFixture {

        [Test]
        [TestCase("eicall", (ushort)0x9519)]
        [TestCase("eijmp", (ushort)0x9419)]
        [TestCase("icall", (ushort)0x9509)]
        [TestCase("ijmp", (ushort)0x9409)]
        [TestCase("ret", (ushort)0x9508)]
        [TestCase("reti", (ushort)0x9518)]
        public void SimpleInstructionTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("rcall ($+2) + 4094", (ushort)0xd7ff)]
        [TestCase("rcall ($+2) - 4096", (ushort)0xd800)]
        [TestCase("rcall ($+2)", (ushort)0xd000)]
        [TestCase("rcall $", (ushort)0xdfff)]
        public void RcallTestTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("rjmp ($+2) + 4094", (ushort)0xc7ff)]
        [TestCase("rjmp ($+2) - 4096", (ushort)0xc800)]
        [TestCase("rjmp ($+2)", (ushort)0xc000)]
        [TestCase("rjmp $", (ushort)0xcfff)]
        public void RjmpTestTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("call 0x1234 * 2", (ushort)0x940e, (ushort)0x1234)]
        [TestCase("call ((1 << 22) - 1) * 2", (ushort)0x95ff, (ushort)0xffff)]
        public void CallTest(string asm, ushort opcode, ushort adr) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode, adr }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("jmp 0x1234 * 2", (ushort)0x940c, (ushort)0x1234)]
        [TestCase("jmp ((1 << 22) - 1) * 2", (ushort)0x95fd, (ushort)0xffff)]
        public void JmpTest(string asm, ushort opcode, ushort adr) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode, adr }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("cpse r31, r0", (ushort)0x11f0)]
        [TestCase("cpse r0, r31", (ushort)0x120f)]
        public void CpseTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
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
        [TestCase("jmp ((1 << 22) - 0) * 2")]
        [TestCase("cpse r0, r32")]
        [TestCase("cpse r32, r0")]
        [TestCase("rcall $+2 + 4096")]
        [TestCase("rcall $+2 - 4098")]
        [TestCase("rjmp $+2 + 4096")]
        [TestCase("rjmp $+2 - 4098")]
        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }

    }
}
