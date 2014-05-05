using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class LabelsTest : BaseTestFixture {

        [Test]
        public void LabelColonTest() {
            var complied = Compile(@"
section code
back:    dw forward
forward: dw back
self:    dw self
");
            Assert.AreEqual(new ushort[] {2, 0, 4}, complied.CodeSection.ReadAsUshorts());
        }

        [Test]
        public void LabelNoColonTest() {
            var complied = Compile(@"
section code
back    dw forward
forward dw back
self    dw self
");
            Assert.AreEqual(new ushort[] { 2, 0, 4 }, complied.CodeSection.ReadAsUshorts());
        }

        [Test]
        public void EmptyLabelTest() {
            const string content = @"
section code
main:
";
            var compiled = Compile(content);
        }

    }
}
