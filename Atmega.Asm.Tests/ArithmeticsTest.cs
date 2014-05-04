using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class ArithmeticsTest : BaseTestFixture {


        [Test]
        [TestCase("adiw")]
        [TestCase("sbiw")]
        public void RegWImm6BoundariesTest(string op) {
            Compile("adiw r24, 0");
            Compile("sbiw r24, 0");

            Compile("adiw r24, 63");
            Compile("sbiw r24, 63");

            try {
                Compile("adiw r24, 64");
                Assert.Fail();
            } catch (TokenException) {
            }

            try {
                Compile("sbiw r24, 64");
                Assert.Fail();
            } catch (TokenException) {
            }
        }
    }
}
