using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class AssemblerTest {

        [Test]
        public void ArithmeticsTest() {
            var content = LoadEmbeded("arithmetics.asm");
            var compiled = new Assembler().Assemble(content);
        }

        [Test]
        public void LogicTest() {
            var content = LoadEmbeded("logic.asm");
            var compiled = new Assembler().Assemble(content);
        }

        [Test]
        public void BitTest() {
            var content = LoadEmbeded("bit.asm");
            var compiled = new Assembler().Assemble(content);
        }

        [Test]
        public void MoveTest() {
            var content = LoadEmbeded("move.asm");
            var compiled = new Assembler().Assemble(content);
        }

        [Test]
        public void BranchTest() {
            var content = LoadEmbeded("branch.asm");
            var compiled = new Assembler().Assemble(content);
        }


        [Test]
        public void EmptyLabelTest() {
            var content = @"
section code
main:
";
            var compiled = new Assembler().Assemble(content);
        }

        [Test]
        [TestCase("breq")]
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
            var compiled = new Assembler().Assemble(content);
        }

        private string LoadEmbeded(string name) {
            var type = GetType();
            var ns = type.Namespace;
            var fullname = ns + ".Samples." + name;
            var stream = type.Assembly.GetManifestResourceStream(fullname);
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
