using Atmega.Asm.Hex;
using NUnit.Framework;

namespace Atmega.Asm.Tests.Hex {
    [TestFixture]
    public class HexFileLineTest {

        [Test]
        public void ParseTest() {
            const string line = ":100B4C00E356EE0F94D210F4689440F81BD2E3569F";
            var res = HexFileLine.Parse(line);

            Assert.IsNotNull(res.Data);
            Assert.AreEqual(16, res.Data.Length);
            Assert.AreEqual(0xb4c, res.Address);
            Assert.AreEqual(HexFileLineType.Data, res.Type);

            Assert.AreEqual(0xe3, res.Data[0]);
            Assert.AreEqual(0x56, res.Data[15]);
        }
    }
}
