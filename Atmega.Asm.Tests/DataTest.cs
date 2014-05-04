using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class DataTest : BaseTestFixture {

        [Test]
        public void DefineBytesTest() {
            var compiled = Compile(@"
section code
db 12
db 0x34, 45, 'abc', 0
");
            Assert.AreEqual(7, compiled.CodeSection.Content.Count);
            Assert.AreEqual(new byte[] { 12, 0x34, 45, (byte)'a', (byte)'b', (byte)'c', 0 }, compiled.CodeSection.Content);
        }


        [Test]
        public void DefineWordsTest() {
            var compiled = Compile(@"
section code
dw 0x1234, 0x5678
");
            Assert.AreEqual(4, compiled.CodeSection.Content.Count);
            Assert.AreEqual(new byte[] { 0x34, 0x12, 0x78, 0x56 }, compiled.CodeSection.Content);
        }
        
        [Test]
        public void ReserveBytesTest() {
            var compiled = Compile(@"
section code
rb 6
db 0x34, 45, 'abc', 0
");
            Assert.AreEqual(12, compiled.CodeSection.Content.Count);
            Assert.AreEqual(new byte[] { 0, 0, 0, 0, 0, 0, 0x34, 45, (byte)'a', (byte)'b', (byte)'c', 0 }, compiled.CodeSection.Content);
        }

        [Test]
        public void ReserveWordsTest() {
            var compiled = Compile(@"
section code
rw 3
db 0x34, 45, 'abc', 0
");
            Assert.AreEqual(12, compiled.CodeSection.Content.Count);
            Assert.AreEqual(new byte[] { 0, 0, 0, 0, 0, 0, 0x34, 45, (byte)'a', (byte)'b', (byte)'c', 0 }, compiled.CodeSection.Content);
        }
    }
}
