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
                //SaveBin(res, Path.GetFileNameWithoutExtension(sourceName) + ".binhex");
                Console.WriteLine("passes: {0}", res.Pass);
                Console.WriteLine("code size: {0} words", (res.CodeSection.Content.Count + 1) / 2);
            } catch (TokenException exc) {
                Console.WriteLine("error: {0}:{1}", exc.Token.Position.File.FileName, exc.Token.Position.Line);
                Console.WriteLine(exc.Token.Position.GetLine());
                Console.WriteLine(exc.Message);
                Environment.Exit(1);
            }
        }

        private static void SaveBin(AsmContext context, string path) {
            using (var writer = new StreamWriter(path, false)) {
                foreach (var bt in context.CodeSection.Content) {
                    writer.Write(FormatByte(bt));
                    writer.Write(" ");
                }
            }
        }

        private static string FormatByte(byte bt) {
            const string hex = "0123456789ABCDEF";
            return hex[bt >> 4].ToString() + hex[bt & 0x0f];
        }

        private static void Usage() {
            Console.WriteLine("Usage: atma -source.asm");
        }
    }
}
