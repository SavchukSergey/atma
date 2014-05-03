using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class AssemblerTest : BaseTestFixture {

        [Test]
        public void ArithmeticsTest() {
            var content = LoadEmbeded("arithmetics.asm");
            var compiled = Compile(content);
        }

        [Test]
        public void LogicTest() {
            var content = LoadEmbeded("logic.asm");
            var compiled = Compile(content);
        }

        [Test]
        public void BitTest() {
            var content = LoadEmbeded("bit.asm");
            var compiled = Compile(content);
        }

        [Test]
        public void MoveTest() {
            var content = LoadEmbeded("move.asm");
            var compiled = Compile(content);
        }

        [Test]
        public void BranchTest() {
            var content = LoadEmbeded("branch.asm");
            var compiled = Compile(content);
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
        [TestCase("breq")]
        [TestCase("brne")]
        [TestCase("brcc")]
        [TestCase("brcs")]
        [TestCase("brhc")]
        [TestCase("brhs")]
        [TestCase("brsh")]
        [TestCase("brlo")]
        [TestCase("brpl")]
        [TestCase("brmi")]
        [TestCase("brge")]
        [TestCase("brlt")]
        [TestCase("brtc")]
        [TestCase("brts")]
        [TestCase("brvc")]
        [TestCase("brvs")]
        [TestCase("brie")]
        [TestCase("brid")]
        public void StatusBranchTest(string op) {
            const string template = @"
section code
main:
.back:
    nop
.self: {Operation} .self
    {Operation} .zero
.zero:
    {Operation} .forward
    nop
.forward:
";
            var content = template.Replace("{Operation}", op);
            var compiled = Compile(content);
        }

        [Test]
        public void FlashTest() {
            var complied = ComplieEmbeded("flash.asm");
        }

        [Test]
        public void RjmpTest() {
            var compiled = Compile(@"
section code
back:
    nop
    rjmp back
self:
    rjmp self
    rjmp zero
zero:
    rjmp forward
    nop
forward:
");
            Assert.AreEqual(new[] {
                0, 0,
                0xfe, 0xcf,
                0xff, 0xcf,
                0x00, 0xc0,
                0x01, 0xc0,
                0x00, 0x00
            }, compiled.CodeSection.Content);
        }

    }
}
