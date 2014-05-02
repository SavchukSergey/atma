using Atmega.Asm.Opcodes;
using NUnit.Framework;

namespace Atmega.Asm.Tests.Tokens {
    [TestFixture]
    public class OpcodeTranslationTest {

        [Test]
        public void Destination32Test() {
            var translation = new OpcodeTranslation();
            Assert.AreEqual(0, translation.Destination32);
            translation.Destination32 = 23;
            Assert.AreEqual(23, translation.Destination32);
        }

        [Test]
        public void Destination16Test() {
            var translation = new OpcodeTranslation();
            Assert.AreEqual(16, translation.Destination16);
            translation.Destination16 = 23;
            Assert.AreEqual(23, translation.Destination16);
        }

        [Test]
        public void Register32Test() {
            var translation = new OpcodeTranslation();
            Assert.AreEqual(0, translation.Register32);
            translation.Register32 = 23;
            Assert.AreEqual(23, translation.Register32);
        }

        [Test]
        public void Imm8Test() {
            var translation = new OpcodeTranslation();
            Assert.AreEqual(0, translation.Imm8);
            translation.Imm8 = 123;
            Assert.AreEqual(123, translation.Imm8);
        }

        [Test]
        public void Port32Test() {
            var translation = new OpcodeTranslation();
            Assert.AreEqual(0, translation.Port32);
            translation.Port32 = 12;
            Assert.AreEqual(12, translation.Port32);
        }

        [Test]
        public void Port64Test() {
            var translation = new OpcodeTranslation();
            Assert.AreEqual(0, translation.Port64);
            translation.Port64 = 54;
            Assert.AreEqual(54, translation.Port64);
        }

        [Test]
        public void BitNumberTest() {
            var translation = new OpcodeTranslation();
            Assert.AreEqual(0, translation.BitNumber);
            translation.BitNumber = 2;
            Assert.AreEqual(2, translation.BitNumber);
        }

        [Test]
        public void StatusBitNumberTest() {
            var translation = new OpcodeTranslation();
            Assert.AreEqual(0, translation.StatusBitNumber);
            translation.StatusBitNumber = 3;
            Assert.AreEqual(3, translation.StatusBitNumber);
        }

        [Test]
        public void Offset7Test() {
            var translation = new OpcodeTranslation();
            Assert.AreEqual(0, translation.Offset7);
            translation.Offset7 = 62;
            Assert.AreEqual(62, translation.Offset7);
            translation.Offset7 = -62;
            Assert.AreEqual(-62, translation.Offset7);
        }

        [Test]
        public void Offset12Test() {
            var translation = new OpcodeTranslation();
            Assert.AreEqual(0, translation.Offset12);
            translation.Offset12 = 620;
            Assert.AreEqual(620, translation.Offset12);
            translation.Offset12 = -620;
            Assert.AreEqual(-620, translation.Offset12);
        }

        [Test]
        public void Offset22HighTest() {
            var translation = new OpcodeTranslation();
            Assert.AreEqual(0, translation.Offset22High);
            translation.Offset22High = 62;
            Assert.AreEqual(62, translation.Offset22High);
        }
    }
}
