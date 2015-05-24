﻿using System.IO;
using Atmega.Asm.Hex;
using Atmega.Hex;
using NUnit.Framework;

namespace Atmega.Asm.Tests.Hex {
    [TestFixture]
    public class HexFileTest {

        [Test]
        public void ParseTest() {
            const string content = @":020000021000EC
:10C20000E0A5E6F6FDFFE0AEE00FE6FCFDFFE6FD93
:10C21000FFFFF6F50EFE4B66F2FA0CFEF2F40EFE90
:10C22000F04EF05FF06CF07DCA0050C2F086F097DF
:10C23000F04AF054BCF5204830592D02E018BB03F9
:020000020000FC
:04000000FA00000200
:00000001FF";
            var file = HexFile.Parse(new StringReader(content));
            Assert.AreEqual(8, file.Lines.Count);
        }
    }
}
