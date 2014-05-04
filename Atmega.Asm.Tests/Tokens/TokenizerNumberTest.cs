using Atmega.Asm.Tokens;
using NUnit.Framework;

namespace Atmega.Asm.Tests.Tokens {
    [TestFixture]
    public class TokenizerNumberTest : BaseTestFixture {

        [Test]
        public void IntegerTest() {
            var tokens = Tokenize("db 123");
            Assert.AreEqual(2, tokens.Count);

            Assert.AreEqual("db", tokens.Read(TokenType.Literal).StringValue);
            Assert.AreEqual(123, tokens.Read(TokenType.Integer).IntegerValue);

            try {
                Tokenize("db 12abc");
                Assert.Fail();
            } catch (TokenException) {
            }
        }

        [Test]
        public void IntegerPrefixedHexTest() {
            var tokens = Tokenize("dw 0x123");
            Assert.AreEqual(2, tokens.Count);

            Assert.AreEqual("dw", tokens.Read(TokenType.Literal).StringValue);
            Assert.AreEqual(0x123, tokens.Read(TokenType.Integer).IntegerValue);

            try {
                Tokenize("dw 0x12qwe3");
                Assert.Fail();
            } catch (TokenException) {
            }
        }

        [Test]
        public void IntegerPostfixedHexTest() {
            var tokens = Tokenize("dw 123h");
            Assert.AreEqual(2, tokens.Count);

            Assert.AreEqual("dw", tokens.Read(TokenType.Literal).StringValue);
            Assert.AreEqual(0x123, tokens.Read(TokenType.Integer).IntegerValue);
            
            try {
                Tokenize("dw 12qwe3h");
                Assert.Fail();
            } catch (TokenException) {
            }
        }


    }
}
