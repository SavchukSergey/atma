using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class AsmParserTest : BaseTestFixture {

        [Test]
        public void ReadReg32Test() {
            var parser = GetParser("r0 r1 r2 r3 r4 r5 r6 r7 r8 r9  r10 r11 r12 r13 r14 r15 r16 r17 r18 r19  r20 r21 r22 r23 r24 r25 r26 r27 r28 r29  r30 r31      r32");
            for (var i = 0; i < 32; i++) {
                if (i >= 0 && i <= 31) {
                    var reg = parser.ReadReg32();
                    Assert.AreEqual(i, reg);
                } else {
                    try {
                        parser.ReadReg32();
                        Assert.Fail();
                    } catch (TokenException) {
                    }
                }
            }

            try {
                GetParser("r32").ReadReg32();
                Assert.Fail();
            } catch (TokenException) {
            }

            try {
                GetParser("rn").ReadReg32();
                Assert.Fail();
            } catch (TokenException) {
            }
        }

        [Test]
        public void ReadReg16Test() {
            var parser = GetParser("r0 r1 r2 r3 r4 r5 r6 r7 r8 r9  r10 r11 r12 r13 r14 r15 r16 r17 r18 r19  r20 r21 r22 r23 r24 r25 r26 r27 r28 r29  r30 r31      r32");
            for (var i = 0; i < 32; i++) {
                if (i >= 16 && i <= 31) {
                    var reg = parser.ReadReg16();
                    Assert.AreEqual(i, reg);
                } else {
                    try {
                        parser.ReadReg16();
                        Assert.Fail();
                    } catch (TokenException) {
                    }
                }
            }

            try {
                GetParser("rn").ReadReg16();
                Assert.Fail();
            } catch (TokenException) {
            }
        }

        private AsmParser GetParser(string content) {
            var tokens = Tokenize(content);
            return new AsmParser(new AsmContext { Queue = tokens });
        }
    }
}
