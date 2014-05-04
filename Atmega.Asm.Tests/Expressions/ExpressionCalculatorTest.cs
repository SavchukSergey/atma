using Atmega.Asm.Expressions;
using NUnit.Framework;

namespace Atmega.Asm.Tests.Expressions {
    [TestFixture]
    public class ExpressionCalculatorTest {

        [Test]
        [TestCase("345", 345)]
        [TestCase("(345)", (345))]
        [TestCase("-345", -345)]
        [TestCase("345 + 452", 345 + 452)]
        [TestCase("(345 + 452)", (345 + 452))]
        [TestCase("345 - 452", 345 - 452)]
        [TestCase("(345 - 452)", (345 - 452))]
        [TestCase("-(345 - 452)", -(345 - 452))]
        [TestCase("345 + 452 + 123", 345 + 452 + 123)]
        [TestCase("345 + 452 * 123", 345 + 452 * 123)]
        [TestCase("345 * 452 + 123", 345 * 452 + 123)]
        [TestCase("(345 + 452) * 123", (345 + 452) * 123)]
        [TestCase("345 * (452 + 123)", 345 * (452 + 123))]
        [TestCase("345 + (452 * 123)", 345 + (452 * 123))]
        [TestCase("(345 * 452) + 123", (345 * 452) + 123)]
        [TestCase("345 + 452 * 123 * 789 + 243", 345 + 452 * 123 * 789 + 243)]
        [TestCase("345 * 452 % 123", 345 * (452 % 123))]
        [TestCase("3 << 5", 3 << 5)]
        [TestCase("0x12f3 | 0x3012", 0x12f3 | 0x3012)]
        [TestCase("0x12f3 & 0x3012", 0x12f3 & 0x3012)]
        [TestCase("(1 << 3) | (1 << 2)", (1 << 3) | (1 << 2))]
        [TestCase("(123*456)*(78*452)", (123 * 456) * (78 * 452))]
        [TestCase("345 + 242, 111", 345 + 242)]
        [TestCase("LOW(345 + 242)", (345 + 242) & 0xff)]
        [TestCase("HIGH(345 + 242)", (345 + 242) >> 8)]
        public void PositiveTests(string expression, long result) {
            var expr = new ExpressionCalculator(null).Parse(expression);
            Assert.AreEqual(result, expr.Evaluate());
        }
    }
}
