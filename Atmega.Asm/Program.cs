using System;
using System.Collections;
using System.IO;
using Atmega.Asm.Hex;
using Atmega.Asm.IO;
using Atmega.Asm.Opcodes;
using Atmega.Hex;

namespace Atmega.Asm {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Atma {0} - Atmega Assembler", typeof(Program).Assembly.GetName().Version);
            if (args.Length == 0) {
                Usage();
                return;
            }
            AtmaArgs atmaArgs;
            try {
                atmaArgs = AtmaArgs.Parse(args);
            } catch {
                Usage();
                return;
            }

            switch (atmaArgs.Action) {
                case AtmaAction.Assemble:
                    Assemble(atmaArgs);
                    break;
                case AtmaAction.Disassemble:
                    Disasm(atmaArgs);
                    break;
                default:
                    Usage();
                    return;
            }

        }

        static void Assemble(AtmaArgs args) {
            var sourceName = args.Source;
            var assembler = new Assembler(new FileAsmSource());
            try {
                var res = assembler.Load(sourceName);
                var hexName = Path.GetFileNameWithoutExtension(sourceName) + ".hex";
                var eepName = Path.GetFileNameWithoutExtension(sourceName) + ".eep";
                var elfName = Path.GetFileNameWithoutExtension(sourceName) + ".elf";
                res.CodeSection.BuildHexFile().Save(hexName);
                res.FlashSection.BuildHexFile().Save(eepName);
                assembler.SaveElf(res, elfName);
                //SaveBin(res, Path.GetFileNameWithoutExtension(sourceName) + ".binhex");
                Console.WriteLine("passes: {0}", res.Pass);
                Console.WriteLine("Segment   Size");
                Console.WriteLine("--------------");
                Console.WriteLine("[code]  {0}", res.CodeSection.WordsCount.ToString().PadLeft(6, ' '));
                Console.WriteLine("[data]  {0}", res.DataSection.BytesCount.ToString().PadLeft(6, ' '));
                Console.WriteLine("[flash] {0}", res.FlashSection.BytesCount.ToString().PadLeft(6, ' '));
            } catch (TokenException exc) {
                Console.WriteLine("error: {0}:{1}", exc.Token.Position.File.FileName, exc.Token.Position.Line);
                Console.WriteLine(exc.Token.Position.GetLine());
                Console.WriteLine(exc.Message);
                Environment.Exit(1);
            }
        }

        static void Disasm(AtmaArgs args) {
            var path = args.Source;
            var output = new StreamWriter(path + ".asm");
            var file = HexFile.Load(path);
            var code = file.GetCode();
            var codeStream = new MemoryStream(code);
            int unknwn = 0;
            while (codeStream.Position < codeStream.Length) {
                output.Write("lbl_{0:x4}: ", codeStream.Position);
                var opcode = BaseOpcode.Parse(codeStream);
                if (opcode is UnknownOpcode) {
                    unknwn++;
                }
                if (opcode != null) {
                    output.WriteLine(opcode.ToString());
                } else {
                    output.WriteLine("unknown");
                }
            }
            output.Flush();
            output.Close();
            Console.WriteLine("Unknown: {0}", unknwn);
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
            Console.WriteLine("Usage: atma source.asm");
        }
    }

    public class AtmaArgs {

        public string Source { get; set; }

        public string Destination { get; set; }

        public AtmaAction Action { get; set; }

        public static AtmaArgs Parse(string[] args) {
            var res = new AtmaArgs();
            foreach (var arg in args) {
                if (!arg.StartsWith("-")) {
                    if (string.IsNullOrWhiteSpace(res.Source)) {
                        res.Source = arg;
                    } else if (string.IsNullOrWhiteSpace(res.Destination)) {
                        res.Destination = arg;
                    } else {
                        throw new InvalidOperationException();
                    }
                }
                switch (arg) {
                    case "-d":
                        res.Action = AtmaAction.Disassemble;
                        break;
                }
            }
            return res;
        }

    }

    public enum AtmaAction {
        Assemble,
        Disassemble
    }
}
