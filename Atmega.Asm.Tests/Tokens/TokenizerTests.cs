using Atmega.Asm.Tokens;
using NUnit.Framework;

namespace Atmega.Asm.Tests.Tokens {
    [TestFixture]
    public class TokenizerTests {

        private TokensQueue Tokenize(string content) {
            return new TokensQueue(new Tokenizer().Read(content));
        }

        [Test]
        public void SimpleInstructionTest() {
            var tokens = Tokenize(" cli");
            Assert.AreEqual(1, tokens.Count);
            Assert.AreEqual("cli", tokens.Read(TokenType.Literal).StringValue);
        }

        [Test]
        public void InstructionRegTest() {
            var tokens = Tokenize(" clr r16");
            Assert.AreEqual(2, tokens.Count);
            Assert.AreEqual("clr", tokens.Read(TokenType.Literal).StringValue);
            Assert.AreEqual("r16", tokens.Read(TokenType.Literal).StringValue);
        }

        [Test]
        public void InstructionRegRegTest() {
            var tokens = Tokenize(" add r16, r22");
            Assert.AreEqual(4, tokens.Count);
            Assert.AreEqual("add", tokens.Read(TokenType.Literal).StringValue);
            Assert.AreEqual("r16", tokens.Read(TokenType.Literal).StringValue);
            Assert.AreEqual(",", tokens.Read(TokenType.Punctuation).StringValue);
            Assert.AreEqual("r22", tokens.Read(TokenType.Literal).StringValue);
        }

        [Test]
        public void StringSingleQuoteTest() {
            var tokens = Tokenize("db 'Test '' String'");
            Assert.AreEqual(2, tokens.Count);

            Assert.AreEqual("db", tokens.Read(TokenType.Literal).StringValue);
            Assert.AreEqual("Test ' String", tokens.Read(TokenType.String).StringValue);
        }

        [Test]
        public void StringDoubleQuoteTest() {
            var tokens = Tokenize("db \"Test \"\" String\"");
            Assert.AreEqual(2, tokens.Count);

            Assert.AreEqual("db", tokens.Read(TokenType.Literal).StringValue);
            Assert.AreEqual("Test \" String", tokens.Read(TokenType.String).StringValue);
        }

        [Test]
        public void IntegerTest() {
            var tokens = Tokenize("db 123");
            Assert.AreEqual(2, tokens.Count);

            Assert.AreEqual("db", tokens.Read(TokenType.Literal).StringValue);
            Assert.AreEqual(123, tokens.Read(TokenType.Integer).IntegerValue);
        }

        [Test]
        public void IntegerPrefixedHexTest() {
            var tokens = Tokenize("dw 0x123");
            Assert.AreEqual(2, tokens.Count);

            Assert.AreEqual("dw", tokens.Read(TokenType.Literal).StringValue);
            Assert.AreEqual(0x123, tokens.Read(TokenType.Integer).IntegerValue);
        }

        [Test]
        public void IntegerPostfixedHexTest() {
            var tokens = Tokenize("dw 123h");
            Assert.AreEqual(2, tokens.Count);

            Assert.AreEqual("dw", tokens.Read(TokenType.Literal).StringValue);
            Assert.AreEqual(0x123, tokens.Read(TokenType.Integer).IntegerValue);
        }
    }
}
