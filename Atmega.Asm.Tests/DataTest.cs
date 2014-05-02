using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class DataTest : BaseTestFixture {

        [Test]
        public void BytesTest() {
            var compiled = Compile(@"
section code
db 12
db 0x34, 45, 'abc', 0
");
            Assert.AreEqual(7, compiled.CodeSection.Content.Count);
            Assert.AreEqual(new byte[] { 12, 0x34, 45, (byte)'a', (byte)'b', (byte)'c', 0 }, compiled.CodeSection.Content);
        }
    }
}
