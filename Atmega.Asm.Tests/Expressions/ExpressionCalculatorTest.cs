using Atmega.Asm.Expressions;
using NUnit.Framework;

namespace Atmega.Asm.Tests.Expressions {
    [TestFixture]
    public class ExpressionCalculatorTest {

        [Test]
        public void NumberTest() {
            var expr = new ExpressionCalculator(null).Parse("345");
            Assert.AreEqual(345, expr.Evaluate());
        }

        [Test]
        public void SumTest() {
            var expr = new ExpressionCalculator(null).Parse("345 + 452");
            Assert.AreEqual(345 + 452, expr.Evaluate());
        }


        [Test]
        public void SumSumTest() {
            var expr = new ExpressionCalculator(null).Parse("345 + 452 + 123");
            Assert.AreEqual(345 + 452 + 123, expr.Evaluate());
        }

        [Test]
        public void SumMulTest() {
            var expr = new ExpressionCalculator(null).Parse("345 + 452 * 123");
            Assert.AreEqual(345 + 452 * 123, expr.Evaluate());
        }

        [Test]
        public void MulSumTest() {
            var expr = new ExpressionCalculator(null).Parse("345 * 452 + 123");
            Assert.AreEqual(345 * 452 + 123, expr.Evaluate());
        }

        [Test]
        public void SumMul2Test() {
            var expr = new ExpressionCalculator(null).Parse("(345 + 452) * 123");
            Assert.AreEqual((345 + 452) * 123, expr.Evaluate());
        }

        [Test]
        public void MulSum2Test() {
            var expr = new ExpressionCalculator(null).Parse("345 * (452 + 123)");
            Assert.AreEqual(345 * (452 + 123), expr.Evaluate());
        }

        [Test]
        public void SumMul3Test() {
            var expr = new ExpressionCalculator(null).Parse("345 + (452 * 123)");
            Assert.AreEqual(345 + (452 * 123), expr.Evaluate());
        }

        [Test]
        public void MulSum3Test() {
            var expr = new ExpressionCalculator(null).Parse("(345 * 452) + 123");
            Assert.AreEqual((345 * 452) + 123, expr.Evaluate());
        }
    }
}
