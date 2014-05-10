using Atmega.Asm.Tokens;

namespace Atmega.Asm.Opcodes.Branch {
    public abstract class BaseOffset22Opcode : BaseOpcode {
        
        protected BaseOffset22Opcode(string opcodeTemplate)
            : base(opcodeTemplate) {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
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
            var translation = new OpcodeTranslation { Opcode = _opcodeTemplate, Offset22High = (byte)(target >> 16) };
            output.EmitCode(translation.Opcode);
            output.EmitCode((ushort)(target & 0xffff));
        }

    }
}
