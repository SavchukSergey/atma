using Atmega.Asm.Tokens;
using NUnit.Framework;

namespace Atmega.Asm.Tests.Tokens {
    public class TokenizerStringsTest : BaseTestFixture {

        [Test]
        public void UnclosedSingleQuoteStringTest() {
            try {
                Tokenize("\r\n db 'asd");
            } catch (TokenException exc) {
                Assert.AreEqual(2, exc.Token.Position.Line);
            }
        }

        [Test]
        public void UnclosedDoubleuoteStringTest() {
            try {
                Tokenize("\r\n db \" asd");
            } catch (TokenException exc) {
                Assert.AreEqual(2, exc.Token.Position.Line);
            }
        }

        [Test]
        public void StringDoubleQuoteTest() {
            var tokens = Tokenize("db \"Test \"\" String\"");
            Assert.AreEqual(2, tokens.Count);

            Assert.AreEqual("db", tokens.Read(TokenType.Literal).StringValue);
            Assert.AreEqual("Test \" String", tokens.Read(TokenType.String).StringValue);
        }


        [Test]
        public void StringSingleQuoteTest() {
            var tokens = Tokenize("db 'Test '' String'");
            Assert.AreEqual(2, tokens.Count);

            Assert.AreEqual("db", tokens.Read(TokenType.Literal).StringValue);
            Assert.AreEqual("Test ' String", tokens.Read(TokenType.String).StringValue);
        }

        [Test]
        public void EscapedTest() {
            var tokens = Tokenize(@"'abs\n\r\a\b\f\t\v\\\0\'\""'");
            Assert.AreEqual("abs\n\r\a\b\f\t\v\\\0\'\"", tokens.Read(TokenType.String).StringValue);
        }

    }
}
