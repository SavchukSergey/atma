using NUnit.Framework;

namespace Atmega.Asm.Tests {
    [TestFixture]
    public class StackTests : BaseTestFixture {
        [Test]
        [TestCase("pop r0", (ushort)0x900f)]
        [TestCase("pop r31", (ushort)0x91ff)]
        public void PopTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("push r0", (ushort)0x920f)]
        [TestCase("push r31", (ushort)0x93ff)]
        public void PushTest(string asm, ushort opcode) {
            var compiled = Compile(asm);
            Assert.AreEqual(new[] { opcode }, compiled.CodeSection.ReadAsUshorts());
        }


        [Test]
        [TestCase("push r0 r1 r31", (ushort)0x920f, (ushort)0x921f, (ushort)0x93ff, TestName = "PushMultipleArguments")]
        [TestCase("push r31 r1 r0", (ushort)0x93ff, (ushort)0x921f, (ushort)0x920f, TestName = "PushMultipleArgumentsReverse")]
        [TestCase("pop r0 r1 r31", (ushort)0x900f, (ushort)0x901f, (ushort)0x91ff, TestName = "PopMultipleArguments")]
        [TestCase("pop r31 r1 r0", (ushort)0x91ff, (ushort)0x901f, (ushort)0x900f, TestName = "PopMultipleArgumentsReverse")]
        public void MultiArgTest(string asm, params ushort[] opcodes) {
            var compiled = Compile(asm);
            Assert.AreEqual(opcodes, compiled.CodeSection.ReadAsUshorts());
        }

        [Test]
        [TestCase("push")]
        [TestCase("pop")]
        public void EmptyArgTest(string asm) {
            Assert.Throws<TokenException>(() => Compile(asm));
        }

    }
}
