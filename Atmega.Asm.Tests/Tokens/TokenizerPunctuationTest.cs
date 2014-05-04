using Atmega.Asm.Tokens;
using NUnit.Framework;

namespace Atmega.Asm.Tests.Tokens {
    public class TokenizerPunctuationTest : BaseTestFixture {

        [Test]
        [TestCase("+", TokenType.Plus)]
        [TestCase("-", TokenType.Minus)]
        [TestCase("*", TokenType.Multiply)]
        [TestCase("/", TokenType.Divide)]
        [TestCase("%", TokenType.Mod)]
        [TestCase("|", TokenType.BitOr)]
        [TestCase("&", TokenType.BitAnd)]
        [TestCase("<<", TokenType.LeftShift)]
        [TestCase(">>", TokenType.RightShift)]
        public void OperatorsTest(string op, TokenType type) {
            var res = Tokenize("1 {Operator} 2".Replace("{Operator}", op));
            Assert.AreEqual(3, res.Count);
            res.Read(TokenType.Integer);
            res.Read(type);
            res.Read(TokenType.Integer);
        }

    }
}
