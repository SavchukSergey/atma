using System.Globalization;
using System.IO;
using Atmega.Asm.Opcodes.Arithmetics;
using Atmega.Asm.Opcodes.Bit;
using Atmega.Asm.Opcodes.Bit.Status;
using Atmega.Asm.Opcodes.Branch;
using Atmega.Asm.Opcodes.Branch.Status;
using Atmega.Asm.Opcodes.Logic;
using Atmega.Asm.Opcodes.Move;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseOpcode {

        protected readonly ushort _opcodeTemplate;

        protected BaseOpcode(string opcodeTemplate) {
            _opcodeTemplate = ParseOpcodeTemplate(opcodeTemplate);
        }

        public virtual void Compile(AsmParser parser, AsmSection output) {
            Parse(parser);
            Compile(output);
        }

        protected virtual void Parse(AsmParser parser) {
        }

        protected virtual void Compile(AsmSection output) {
        }

        protected static ushort ParseOpcodeTemplate(string template) {
            ushort val = 0;
            for (var i = 0; i < 16; i++) {
                var bit = template[i];
                if (bit == '1') {
                    val = (ushort)(val * 2 + 1);
                } else {
                    val = (ushort)(val * 2);
                }
            }
            return val;
        }

        public static BaseOpcode Parse(Stream stream) {
            var bytecode = ReadUShort(stream);
            var translation = new OpcodeTranslation { Opcode = bytecode };

            switch (bytecode) {
                case 0x0000:
                    return new NopOpcode();
                case 0x9508:
                    return new RetOpcode();
                case 0x9518:
                    return new RetiOpcode();
                case 0x95a8:
                    return new WdrOpcode();
            }

            if ((bytecode & 0xff0f) == 0x9408) {
                return BaseStatusBitOpcode.FromOpcode(bytecode);
            }


            if ((bytecode & 0xff00) == 0x9700) {
                return new SbiwOpcode { Register = translation.RegW24, Value = translation.Imm6 };
            }
            if ((bytecode & 0xff00) == 0x9600) {
                return new AdiwOpcode { Register = translation.RegW24, Value = translation.Imm6 };
            }
            if ((bytecode & 0xff00) == 0x0100) {
                return new MovwOpcode { Register = translation.Register16Word, Destination = translation.Destination16Word };
            }
            if ((bytecode & 0xff00) == 0x9800) {
                return new CbiOpcode { Port = translation.Port32, Bit = translation.BitNumber };
            }
            if ((bytecode & 0xff00) == 0x9a00) {
                return new SbiOpcode { Port = translation.Port32, Bit = translation.BitNumber };
            }
            if ((bytecode & 0xff00) == 0x9b00) {
                return new SbisOpcode { Port = translation.Port32, Bit = translation.BitNumber };
            }
            if ((bytecode & 0xff00) == 0x9900) {
                return new SbicOpcode { Port = translation.Port32, Bit = translation.BitNumber };
            }

            if ((bytecode & 0xee0c) == 0x8200) {
                return new StOpcode { Source = translation.Destination32, Operand = translation.IndirectOperand };
            }
            if ((bytecode & 0xee0c) == 0x8000) {
                return new LdOpcode { Destination = translation.Destination32, Operand = translation.IndirectOperand };
            }

            if ((bytecode & 0xfe08) == 0xf800) {
                return new BldOpcode { Register = translation.Destination32, Bit = translation.BitNumber };
            }
            if ((bytecode & 0xfe08) == 0xfa00) {
                return new BstOpcode { Register = translation.Destination32, Bit = translation.BitNumber };
            }
            if ((bytecode & 0xfe08) == 0xfc00) {
                return new SbrcOpcode { Register = translation.Destination32, Bit = translation.BitNumber };
            }
            if ((bytecode & 0xfe08) == 0xfe00) {
                return new SbrsOpcode { Register = translation.Destination32, Bit = translation.BitNumber };
            }

            //if ((bytecode & 0xfe0e) == 0x9004) {
            //    return new LpmOpcode { Register = translation.Destination32, Increment = translation.Increment };
            //}
            if ((bytecode & 0xffef) == 0x95e8) {
                return new SpmOpcode { PostIncrement = translation.SpmIncrement };
            }
            //if ((bytecode & 0xfe0f) == 0x9000) {
            //    return new LdsOpcode { Register = translation.Destination32, Address = ReadUShort(stream) };
            //}
            if ((bytecode & 0xfe0f) == 0x920f) {
                return new PushOpcode { Register = translation.Destination32 };
            }
            if ((bytecode & 0xfe0f) == 0x900f) {
                return new PopOpcode { Register = translation.Destination32 };
            }
            if ((bytecode & 0xfe00) == 0x9400) {
                var subop = bytecode & 0xf;
                switch (subop) {
                    case 0x00:
                        return new ComOpcode { Register = translation.Destination32 };
                    case 0x01:
                        return new NegOpcode { Register = translation.Destination32 };
                    case 0x03:
                        return new IncOpcode { Register = translation.Destination32 };
                    case 0x05:
                        return new AsrOpcode { Register = translation.Destination32 };
                    case 0x06:
                        return new LsrOpcode { Register = translation.Destination32 };
                    case 0x07:
                        return new RorOpcode { Register = translation.Destination32 };
                    case 0x0a:
                        return new DecOpcode { Register = translation.Destination32 };
                }
            }

            if ((bytecode & 0xfc00) == 0x0400) {
                return new CpcOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            }
            if ((bytecode & 0xfc00) == 0x0800) {
                return new SbcOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            }
            if ((bytecode & 0xfc00) == 0x0c00) {
                return new AddOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            }
            if ((bytecode & 0xfc00) == 0x1000) {
                return new CpseOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            }
            if ((bytecode & 0xfc00) == 0x1400) {
                return new CpOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            }
            if ((bytecode & 0xfc00) == 0x1800) {
                return new SubOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            }
            if ((bytecode & 0xfc00) == 0x1c00) {
                return new AdcOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            }
            if ((bytecode & 0xfc00) == 0x2000) {
                return new AndOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            }
            if ((bytecode & 0xfc00) == 0x2400) {
                return new EorOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            }
            if ((bytecode & 0xfc00) == 0x2800) {
                return new OrOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            }
            if ((bytecode & 0xfc00) == 0x2c00) {
                return new MovOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            }

            if ((bytecode & 0xfc00) == 0x9c00) {
                return new MulOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            }

            if ((bytecode & 0xf800) == 0xb800) {
                return new OutOpcode { Port = translation.Port64, Register = translation.Destination32 };
            }
            if ((bytecode & 0xf800) == 0xb000) {
                return new InOpcode { Port = translation.Port64, Register = translation.Destination32 };
            }
            if ((bytecode & 0xf800) == 0xf000) {
                return BaseStatusBitOffset7Opcode.FromOpcode(bytecode);
            }

            if ((bytecode & 0xf000) == 0xc000) {
                return new RjmpOpcode { Delta = (short) (translation.Offset12 + 2) };
            }
            if ((bytecode & 0xf000) == 0xd000) {
                return new RcallOpcode { Delta = (short) (translation.Offset12  + 2)};
            }
            if ((bytecode & 0xfe0e) == 0x940e) {
                var high = translation.Offset22High;
                var low = ReadUShort(stream);
                return new CallOpcode { Target = ((high << 16) + low) << 1 };
            }
            if ((bytecode & 0xfe0e) == 0x940c) {
                var high = translation.Offset22High;
                var low = ReadUShort(stream);
                return new JmpOpcode { Target = ((high << 16) + low) << 1 };
            }

            if ((bytecode & 0xf000) == 0x3000) {
                return new CpiOpcode { Register = translation.Destination16, Value = translation.Imm8 };
            }
            if ((bytecode & 0xf000) == 0x4000) {
                return new SbciOpcode { Register = translation.Destination16, Value = translation.Imm8 };
            }
            if ((bytecode & 0xf000) == 0x5000) {
                return new SubiOpcode { Register = translation.Destination16, Value = translation.Imm8 };
            }
            if ((bytecode & 0xf000) == 0x6000) {
                return new OriOpcode { Register = translation.Destination16, Value = translation.Imm8 };
            }
            if ((bytecode & 0xf000) == 0x7000) {
                return new AndiOpcode { Register = translation.Destination16, Value = translation.Imm8 };
            }
            if ((bytecode & 0xf000) == 0xe000) {
                return new LdiOpcode { Register = translation.Destination16, Value = translation.Imm8 };
            }

            //if ((bytecode & 0xd200) == 0x8000) {
            //    return new LddOpCode { Register = translation.Destination32, BaseRegister = translation.YZSelector, Delta = translation.YZOffset };
            //}
            //if ((bytecode & 0xd200) == 0x8200) {
            //    return new StdOpCode { Register = translation.Destination32, BaseRegister = translation.YZSelector, Delta = translation.YZOffset };
            //}



            return new UnknownOpcode { Opcode = bytecode };
        }

        private static ushort ReadUShort(Stream stream) {
            var low = stream.ReadByte();
            var hi = stream.ReadByte();
            return (ushort)((hi << 8) + low);
        }

        protected static string FormatPort(byte port) {
            switch (port) {
                case 21:
                    return "MCUCR";
                case 27:
                    return "GICR";

                case 0x10:
                    return "PIND";
                case 0x11:
                    return "DDRD";
                case 0x12:
                    return "PORTD";

                case 0x1c:
                    return "EECR";
                case 0x39:
                    return "TIMSK";
                case 0x3a:
                    return "GIFR";
                case 0x3b:
                    return "GICR";
                case 61:
                    return "SPL";
                case 62:
                    return "SPH";
                case 63:
                    return "SREG";
                default:
                    return port.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected string FormatPortBit(byte port, byte bit) {
            return bit.ToString();
        }

        protected string FormatStatusBit(byte bit) {
            return bit.ToString();
        }

        protected static string FormatRegister(byte reg) {
            return "r" + reg;
        }

        protected static string FormatWordRegister(byte reg) {
            return FormatRegister((byte)(reg + 1)) + ':' + FormatRegister(reg);
        }

        protected string FormatOffset(int delta, int commandSize) {
            var d = (delta + commandSize) * 2;
            if (d == 0) return "$";
            if (d < 0) return "$" + d;
            return "$+" + d;
        }
        protected string FormatBranchTarget(int target) {
            return string.Format("0x{0:x}", target);
        }

    }
}
