using System;
using System.IO;
using Atmega.Asm.Opcodes.Arithmetics;
using Atmega.Asm.Opcodes.Bit;
using Atmega.Asm.Opcodes.Branch;
using Atmega.Asm.Opcodes.Logic;
using Atmega.Asm.Opcodes.Move;

namespace Atmega.Asm.Opcodes {
    public abstract class BaseOpcode {

        protected readonly ushort _opcodeTemplate;

        protected BaseOpcode(string opcodeTemplate) {
            _opcodeTemplate = ParseOpcodeTemplate(opcodeTemplate);
        }

        public abstract void Compile(AsmParser parser, AsmSection output);

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
            //var translation = new OpcodeTranslation { Opcode = bytecode };

            //switch (bytecode) {
            //    case 0x0000:
            //        return new NopOpcode();

            //    case 0x9468:
            //        return new SetOpcode();
            //    case 0x94e8:
            //        return new CltOpcode();

            //    case 0x9478:
            //        return new SeiOpcode();
            //    case 0x94f8:
            //        return new CliOpcode();

            //    case 0x9508:
            //        return new RetOpcode();
            //}
            //if ((bytecode & 0xff00) == 0x9700) {
            //    return new SbiwOpcode { Register = translation.IndirectWordRegister, Value = translation.UImm6 };
            //}
            //if ((bytecode & 0xff00) == 0x9600) {
            //    return new AdiwOpcode { Register = translation.IndirectWordRegister, Value = translation.UImm6 };
            //}
            //if ((bytecode & 0xff00) == 0x0100) {
            //    return new MovwOpcode { Source = translation.SourceWordRegister, Destination = translation.DestinationWordRegister };
            //}
            //if ((bytecode & 0xff00) == 0x9800) {
            //    return new CbiOpcode { Port = translation.Port32, Bit = translation.BitNumber };
            //}
            //if ((bytecode & 0xff00) == 0x9a00) {
            //    return new SbiOpcode { Port = translation.Port32, Bit = translation.BitNumber };
            //}
            //if ((bytecode & 0xfe00) == 0x9200) {
            //    var target = GetIndirectAddress(translation);
            //    return new StOpcode { Register = translation.Destination32, Target = target };
            //}

            //if ((bytecode & 0xfe08) == 0xf800) {
            //    return new BldOpcode { Register = translation.Destination32, Bit = translation.BitNumber };
            //}
            //if ((bytecode & 0xfe08) == 0xfa00) {
            //    return new BstOpcode { Register = translation.Destination32, Bit = translation.BitNumber };
            //}
            //if ((bytecode & 0xfe08) == 0xfc00) {
            //    return new SbrcOpcode { Register = translation.Destination32, Bit = translation.BitNumber };
            //}
            //if ((bytecode & 0xfe08) == 0xfe00) {
            //    return new SbrsOpcode { Register = translation.Destination32, Bit = translation.BitNumber };
            //}

            //if ((bytecode & 0xfe0e) == 0x9004) {
            //    return new LpmOpcode { Register = translation.Destination32, Increment = translation.Increment };
            //}
            //if ((bytecode & 0xfe0f) == 0x9000) {
            //    return new LdsOpcode { Register = translation.Destination32, Address = ReadUShort(stream) };
            //}
            //if ((bytecode & 0xfe0f) == 0x920f) {
            //    return new PushOpcode { Register = translation.Destination32 };
            //}
            //if ((bytecode & 0xfe0f) == 0x900f) {
            //    return new PopOpcode { Register = translation.Destination32 };
            //}
            //if ((bytecode & 0xfe00) == 0x9400) {
            //    var subop = bytecode & 0xf;
            //    switch (subop) {
            //        case 0x03:
            //            return new IncOpcode { Register = GetRdDestination(bytecode) };
            //        case 0x0a:
            //            return new DecOpcode { Register = GetRdDestination(bytecode) };
            //    }
            //}

            //if ((bytecode & 0xfc00) == 0x0400) {
            //    return new CpcOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            //}
            //if ((bytecode & 0xfc00) == 0x0800) {
            //    return new SbcOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            //}
            //if ((bytecode & 0xfc00) == 0x0c00) {
            //    return new AddOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            //}
            //if ((bytecode & 0xfc00) == 0x1400) {
            //    return new CpOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            //}
            //if ((bytecode & 0xfc00) == 0x1800) {
            //    return new SubOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            //}
            //if ((bytecode & 0xfc00) == 0x2400) {
            //    return new EorOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            //}
            //if ((bytecode & 0xfc00) == 0x1c00) {
            //    return new AdcOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            //}
            //if ((bytecode & 0xfc00) == 0x2800) {
            //    return new OrOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            //}
            //if ((bytecode & 0xfc00) == 0x9c00) {
            //    return new MulOpcode { Register = translation.Register32, Destination = translation.Destination32 };
            //}

            //if ((bytecode & 0xf800) == 0xb800) {
            //    var target = GetRdDestination(bytecode);
            //    return new OutOpcode { Port = translation.Port64, Register = target };
            //}
            //if ((bytecode & 0xf800) == 0xb000) {
            //    var target = GetRdDestination(bytecode);
            //    return new InOpcode { Port = translation.Port64, Register = target };
            //}
            //if ((bytecode & 0xf800) == 0xf000) {
            //    return GetBranchOpcode(translation);
            //}

            //if ((bytecode & 0xf000) == 0xc000) {
            //    return new RjmpOpcode { Offset = translation.Offset12 };
            //}
            //if ((bytecode & 0xf000) == 0xd000) {
            //    return new RcallOpcode { Offset = translation.Offset12 };
            //}

            //if ((bytecode & 0xf000) == 0x3000) {
            //    var reg = GetKdDestination(bytecode);
            //    return new CpiOpcode { Register = reg, Value = translation.Imm8 };
            //}
            //if ((bytecode & 0xf000) == 0x4000) {
            //    var reg = GetKdDestination(bytecode);
            //    return new SbciOpcode { Register = reg, Value = translation.Imm8 };
            //}
            //if ((bytecode & 0xf000) == 0x5000) {
            //    var reg = GetKdDestination(bytecode);
            //    return new SubiOpcode { Register = reg, Value = translation.Imm8 };
            //}
            //if ((bytecode & 0xf000) == 0x6000) {
            //    var reg = GetKdDestination(bytecode);
            //    return new OriOpcode { Register = reg, Value = translation.Imm8 };
            //}
            //if ((bytecode & 0xf000) == 0x7000) {
            //    var reg = GetKdDestination(bytecode);
            //    return new AndiOpcode { Register = reg, Value = translation.Imm8 };
            //}
            //if ((bytecode & 0xf000) == 0xe000) {
            //    var reg = GetKdDestination(bytecode);
            //    var value = GetKdValue(bytecode);
            //    if (value != 255) {
            //        return new LdiOpcode { Register = reg, Value = translation.Imm8 };
            //    }
            //}

            //if ((bytecode & 0xd200) == 0x8000) {
            //    return new LddOpCode { Register = translation.Destination32, BaseRegister = translation.YZSelector, Offset = translation.YZOffset };
            //}
            //if ((bytecode & 0xd200) == 0x8200) {
            //    return new StdOpCode { Register = translation.Destination32, BaseRegister = translation.YZSelector, Offset = translation.YZOffset };
            //}
            return new UnknownOpcode { Opcode = bytecode };
            throw new ArgumentException();
        }

        private static ushort ReadUShort(Stream stream) {
            var low = stream.ReadByte();
            var hi = stream.ReadByte();
            return (ushort)((hi << 8) + low);
        }
    }
}
