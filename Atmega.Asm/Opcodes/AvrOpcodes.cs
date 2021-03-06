﻿using Atmega.Asm.Opcodes.Arithmetics;
using Atmega.Asm.Opcodes.Bit;
using Atmega.Asm.Opcodes.Bit.Status;
using Atmega.Asm.Opcodes.Branch;
using Atmega.Asm.Opcodes.Branch.Status;
using Atmega.Asm.Opcodes.Logic;
using Atmega.Asm.Opcodes.Move;

namespace Atmega.Asm.Opcodes {
    public static class AvrOpcodes {

        public static BaseOpcode Get(string literal) {
            switch (literal.ToLower()) {
                case "adc": return new AdcOpcode();
                case "add": return new AddOpcode();
                case "sub": return new SubOpcode();
                case "sbc": return new SbcOpcode();
                case "adiw": return new AdiwOpcode();
                case "sbiw": return new SbiwOpcode();
                case "mul": return new MulOpcode();
                case "muls": return new MulsOpcode();
                case "mulsu": return new MulsuOpcode();
                case "fmul": return new FmulOpcode();
                case "fmuls": return new FmulsOpcode();
                case "fmulsu": return new FmulsuOpcode();

                case "inc": return new IncOpcode();
                case "dec": return new DecOpcode();

                case "com": return new ComOpcode();
                case "neg": return new NegOpcode();
                case "des": return new DesOpcode();

                case "subi": return new SubiOpcode();
                case "sbci": return new SbciOpcode();

                case "cp": return new CpOpcode();
                case "cpi": return new CpiOpcode();
                case "cpc": return new CpcOpcode();

                case "and": return new AndOpcode();
                case "andi": return new AndiOpcode();
                case "or": return new OrOpcode();
                case "ori": return new OriOpcode();
                case "eor": return new EorOpcode();
                case "cbr": return new CbrOpcode();
                case "sbr": return new SbrOpcode();

                case "clr": return new ClrOpcode();
                case "ser": return new SerOpcode();
                case "tst": return new TstOpcode();

                case "clc": return new ClcOpcode();
                case "sec": return new SecOpcode();
                case "cln": return new ClnOpcode();
                case "sen": return new SenOpcode();
                case "clz": return new ClzOpcode();
                case "sez": return new SezOpcode();
                case "cli": return new CliOpcode();
                case "sei": return new SeiOpcode();
                case "cls": return new ClsOpcode();
                case "ses": return new SesOpcode();
                case "clv": return new ClvOpcode();
                case "sev": return new SevOpcode();
                case "clt": return new CltOpcode();
                case "set": return new SetOpcode();
                case "clh": return new ClhOpcode();
                case "seh": return new SehOpcode();
                case "bclr": return new BclrOpcode();
                case "bset": return new BsetOpcode();

                case "cbi": return new CbiOpcode();
                case "sbi": return new SbiOpcode();
                case "bst": return new BstOpcode();
                case "bld": return new BldOpcode();

                case "lsl": return new LslOpcode();
                case "lsr": return new LsrOpcode();
                case "rol": return new RolOpcode();
                case "ror": return new RorOpcode();
                case "asr": return new AsrOpcode();

                case "mov": return new MovOpcode();
                case "movw": return new MovwOpcode();
                case "ldi": return new LdiOpcode();
                case "lds": return new LdsOpcode();
                case "sts": return new StsOpcode();
                case "ldd": return new LddOpcode();
                case "std": return new StdOpcode();
                case "ld": return new LdOpcode();
                case "st": return new StOpcode();
                case "swap": return new SwapOpcode();
                case "push": return new PushOpcode();
                case "pop": return new PopOpcode();
                case "spm": return new SpmOpcode();
                case "in": return new InOpcode();
                case "out": return new OutOpcode();
                case "lpm": return new LpmOpcode();
                case "elpm": return new ElpmOpcode();
                case "lac": return new LacOpcode();
                case "las": return new LasOpcode();
                case "lat": return new LatOpcode();
                case "xch": return new XchOpcode();

                case "sbrc": return new SbrcOpcode();
                case "sbrs": return new SbrsOpcode();
                case "ijmp": return new IjmpOpcode();
                case "icall": return new IcallOpcode();
                case "eijmp": return new EijmpOpcode();
                case "eicall": return new EicallOpcode();
                case "ret": return new RetOpcode();
                case "reti": return new RetiOpcode();
                case "jmp": return new JmpOpcode();
                case "call": return new CallOpcode();
                case "rjmp": return new RjmpOpcode();
                case "rcall": return new RcallOpcode();

                case "cpse": return new CpseOpcode();
                case "sbic": return new SbicOpcode();
                case "sbis": return new SbisOpcode();

                case "breq": return new BreqOpcode();
                case "brne": return new BrneOpcode();
                case "brcc": return new BrccOpcode();
                case "brcs": return new BrcsOpcode();
                case "brhc": return new BrhcOpcode();
                case "brhs": return new BrhsOpcode();
                case "brsh": return new BrshOpcode();
                case "brlo": return new BrloOpcode();
                case "brpl": return new BrplOpcode();
                case "brmi": return new BrmiOpcode();
                case "brge": return new BrgeOpcode();
                case "brlt": return new BrltOpcode();
                case "brtc": return new BrtcOpcode();
                case "brts": return new BrtsOpcode();
                case "brvc": return new BrvcOpcode();
                case "brvs": return new BrvsOpcode();
                case "brie": return new BrieOpcode();
                case "brid": return new BridOpcode();
                case "brbc": return new BrbcOpcode();
                case "brbs": return new BrbsOpcode();

                case "nop": return new NopOpcode();
                case "wdr": return new WdrOpcode();
                case "sleep": return new SleepOpcode();
                case "break": return new BreakOpcode();

                default: return null;
            }
        }

    }
}
