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

        [Test]
        public void ModTest() {
            var expr = new ExpressionCalculator(null).Parse("345 * 452 % 123");
            Assert.AreEqual(345 * (452 % 123), expr.Evaluate());
        }

        [Test]
        public void ShiftLeftTest() {
            var expr = new ExpressionCalculator(null).Parse("3 << 5");
            Assert.AreEqual(3 << 5, expr.Evaluate());
        }

        [Test]
        public void ShiftOrShiftTest() {
            var expr = new ExpressionCalculator(null).Parse("(1 << 3) | (1 << 2)");
            Assert.AreEqual((1 << 3) | (1 << 2), expr.Evaluate());
        }
    }
}
