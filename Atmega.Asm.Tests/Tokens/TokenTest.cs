using Atmega.Asm.Tokens;
using NUnit.Framework;

namespace Atmega.Asm.Tests.Tokens {
    [TestFixture]
    public class TokenTest {

        [Test]
        public void ParseRegisterTest() {
            for (var i = 0; i < 255; i++) {
                var lowerToken = new Token { StringValue = "r" + i };
                var lowerRes = lowerToken.ParseRegister();
                Assert.AreEqual(i < 32 ? i : 255, lowerRes);

                var upperToken = new Token { StringValue = "R" + i };
                var upperRes = upperToken.ParseRegister();
                Assert.AreEqual(i < 32 ? i : 255, upperRes);
            }
        }
    }
}
