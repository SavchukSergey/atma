using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class DirectivesTest : BaseTestFixture {

        [Test]
        public void EquTest() {
            var compiled = Compile(@"
Name equ adc r12, r12
");
            Assert.AreEqual(0, compiled.Code.Count);

            compiled = Compile(@"
Name equ adc r12, r12
Name
");
            Assert.AreEqual(2, compiled.Code.Count);
        }

    }
}
