using System;
using System.IO;
using Atmega.Asm.IO;

namespace Atmega.Asm {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Atma - Atmega Assembler");
            if (args.Length == 0) {
                Usage();
                return;
            }
            var sourceName = args[0];
            var assembler = new Assembler(new FileAsmSource());
            try {
                var res = assembler.Load(sourceName);
                var hexName = Path.GetFileNameWithoutExtension(sourceName) + ".hex";
                var hex = res.BuildHexFile();
                hex.Save(hexName);
                Console.WriteLine("passes: {0}", res.Pass);
                Console.WriteLine("code size: {0}", res.CodeSection.Content.Count);
            } catch (TokenException exc) {
                Console.WriteLine("error: {0}:{1}", exc.Token.Position.File, exc.Token.Position.Line);
                Console.WriteLine(exc.Message);
                Environment.Exit(1);
            }
        }

        private static void Usage() {
            Console.WriteLine("Usage: atma -source.asm");
        }
    }
}
