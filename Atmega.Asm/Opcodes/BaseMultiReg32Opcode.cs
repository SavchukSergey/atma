using System.Collections.Generic;
using System.Linq;

namespace Atmega.Asm.Opcodes {
    public class BaseMultiReg32Opcode : BaseOpcode {

        private readonly IList<byte> _registers = new List<byte>();

        public IList<byte> Registers {
            get { return _registers; }
        }

        public byte Register {
            get {
                if (_registers.Count == 0) return 0;
                return _registers[0];
            }
            set {
                _registers.Clear();
                _registers.Add(value);
            }
        }

        protected BaseMultiReg32Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        protected override void Parse(AsmParser parser) {
            do {
                var dest = parser.ReadReg32();
                Registers.Add(dest);
            } while (!parser.IsEndOfLine);
        }

        protected override void Compile(AsmSection output) {
            foreach (var reg in Registers) {
                var translation = new OpcodeTranslation {
                    Opcode = _opcodeTemplate,
                    Destination32 = reg
                };
                output.EmitCode(translation.Opcode);
            }
        }

        protected string FormatRegisters(IList<byte> registers) {
            return string.Join(" ", registers.Select(FormatRegister).ToArray());
        }
    }
}
