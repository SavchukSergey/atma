using System;

namespace Atmega.Asm {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Atma - Atmega Assembler");
            if (args.Length == 0)
            {
                Usage();
                return;
            }
            var sourceName = args[1];
        }

        private static void Usage()
        {
            throw new NotImplementedException();
        }
    }
}
