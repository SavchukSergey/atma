using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class DirectivesTest : BaseTestFixture {

        [Test]
        public void EquTest() {
            var compiled = Compile(@"
Name equ adc r12, r12
");
            Assert.AreEqual(0, compiled.CodeSection.Content.Count);

            compiled = Compile(@"
Name equ adc r12, r12
Name
");
            Assert.AreEqual(2, compiled.CodeSection.Content.Count);
        }

        [Test]
        public void OrgTest() {
            var complied = Compile(@"
org 10 * 20
");
            Assert.AreEqual(200, complied.CodeSection.Offset);
        }
    }
}
