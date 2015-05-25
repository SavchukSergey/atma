using System;
using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Branch {
    public abstract class BaseOffset22Opcode : BaseOpcode {
        private int _target;

        public int Target {
            get { return _target; }
            set {
                if ((value & 0x1) > 0) {
                    throw new InvalidOperationException("invalid absolute jump");
                }
                value >>= 1;
                //todo: for 64k devices limit is 64k
                if (value < 0 || value >= (1 << 22)) {
                    throw new InvalidOperationException("jump beyond 4m boundary");
                }
                value <<= 1;
                _target = value;
            }
        }

        protected BaseOffset22Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        protected override void Parse(AsmParser parser) {
            Token firstToken;
            var target = parser.CalculateExpression(out firstToken);

            if ((target & 0x1) > 0) {
                throw new TokenException("invalid absolute jump", firstToken);
            }
            target /= 2;

            //todo: for 64k devices limit is 64k
            if (target < 0 || target >= (1 << 22)) {
                throw new TokenException("jump beyond 4m boundary", firstToken);
            }

            Target = (int)(target << 1);
        }

        protected override void Compile(AsmSection output) {
            var target = Target / 2;
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Offset22High = (byte)(target >> 16) };
            output.EmitCode(translation.Opcode);
            output.EmitCode((ushort)(target & 0xffff));
        }

    }
}
