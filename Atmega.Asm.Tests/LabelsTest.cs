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
            Assert.AreEqual(new ushort[] { 2, 0, 4 }, complied.CodeSection.ReadAsUshorts());
        }

        [Test]
        public void LabelNoColonTest() {
            var compiled = Compile(@"
section code
back    dw forward
forward dw back
self    dw self
");
            Assert.AreEqual(new ushort[] { 2, 0, 4 }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        public void EmptyLabelTest() {
            const string content = @"
section code
main:
";
            var compiled = Compile(content);
        }

        [Test]
        public void LocalLabelsTest() {
            const string content = @"
section code
main dw main
.a dw .a
.b dw .b
main2:
.a dw .a
.b dw .b
dw main.b
";
            var compiled = Compile(content);
            Assert.AreEqual(new ushort[] { 0, 2, 4, 6, 8, 4 }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        public void DupLocalLabelsTest() {
            const string content = @"
section code
main dw main
.a dw .a
.a dw .a
";
            try {
                Compile(content);
                Assert.Fail("duplicated local labels must throw error");
            } catch (TokenException) {
            }
        }

        [Test]
        public void DupGlobalLabelsTest() {
            const string content = @"
section code
main dw main
main dw main
";
            try {
                Compile(content);
                Assert.Fail("duplicated global labels must throw error");
            } catch (TokenException) {
            }
        }

        [Test]
        public void OrphanLocalLabelsTest() {
            const string content = @"
section code
.a dw .a
";
            try {
                Compile(content);
                Assert.Fail("orphan local labels must throw error");
            } catch (TokenException) {
            }
        }
    }
}
