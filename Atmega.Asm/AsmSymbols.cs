using System.Collections.Generic;
using Atmega.Asm.Tokens;

namespace Atmega.Asm {
    public class AsmSymbols {
        
        private readonly Dictionary<string, IList<Token>> _symbolicConstants = new Dictionary<string, IList<Token>>();
        private readonly Dictionary<string, ushort> _labels = new Dictionary<string, ushort>();

        public Dictionary<string, IList<Token>> SymbolicConstants {
            get { return _symbolicConstants; }
        }

        public Dictionary<string, ushort> Labels {
            get { return _labels; }
        }
    }
}
