using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class BranchTest : BaseTestFixture {

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
        [TestCase("breq")]
        [TestCase("brne")]
        [TestCase("brcc")]
        [TestCase("brcs")]
        [TestCase("brhc")]
        [TestCase("brhs")]
        [TestCase("brsh")]
        [TestCase("brlo")]
        [TestCase("brpl")]
        [TestCase("brmi")]
        [TestCase("brge")]
        [TestCase("brlt")]
        [TestCase("brtc")]
        [TestCase("brts")]
        [TestCase("brvc")]
        [TestCase("brvs")]
        [TestCase("brie")]
        [TestCase("brid")]
        public void StatusBranchTest(string op) {
            const string template = @"
section code
main:
.back:
    nop
.self: {Operation} .self
    {Operation} .zero
.zero:
    {Operation} .forward
    nop
.forward:
";
            var content = template.Replace("{Operation}", op);
            var compiled = Compile(content);
        }


        [Test]
        [TestCase("brbc 0, ($+2)+64*2")]
        [TestCase("brbc 0, ($+2)-65*2")]
        [TestCase("brbc 8, ($+2)+0*2")]
        public void FailTest(string opcode) {
            try {
                Compile(opcode);
                Assert.Fail("Must fail");
            } catch (TokenException) {
            }
        }

    }
}
