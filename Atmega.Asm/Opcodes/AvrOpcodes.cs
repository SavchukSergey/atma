using Atmega.Asm.Opcodes.Arithmetics;
using Atmega.Asm.Opcodes.Bit;
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
                case "mul": return new MulOpcode();

                case "inc": return new IncOpcode();
                case "dec": return new DecOpcode();

                case "com": return new ComOpcode();
                case "neg": return new NegOpcode();

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
                case "ldi": return new LdiOpcode();
                case "lds": return new LdsOpcode();
                case "sts": return new StsOpcode();
                case "swap": return new SwapOpcode();
                case "push": return new PushOpcode();
                case "pop": return new PopOpcode();
                case "spm": return new SpmOpcode();
                case "in": return new InOpcode();
                case "out": return new OutOpcode();

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
                
                case "cpse": return new CpseOpcode();

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
                
                case "nop": return new NopOpcode();
                case "wdr": return new WdrOpcode();
                case "sleep": return new SleepOpcode();
                case "break": return new BreakOpcode();

                default: return null;
            }
        }

        /*
             1001010111001000   lpm     ?
             1001000ddddd010+   lpm     r,z
             1001010111011000   elpm    ?
             1001000ddddd011+   elpm    r,z

             10010110KKddKKKK   adiw    w,K
             10010111KKddKKKK   sbiw    w,K
             10011001pppppsss   sbic    p,s
             10011011pppppsss   sbis    p,s
             111101lllllllsss   brbc    s,l
             111100lllllllsss   brbs    s,l
             1101LLLLLLLLLLLL   rcall   L
             1100LLLLLLLLLLLL   rjmp    L
             00000001ddddrrrr   movw    v,v
             00000010ddddrrrr   muls    d,d
             000000110ddd0rrr   mulsu   a,a
             000000110ddd1rrr   fmul    a,a
             000000111ddd0rrr   fmuls   a,a
             000000111ddd1rrr   fmulsu  a,a
             10o0oo0dddddbooo   ldd     r,b
             100!000dddddee-+   ld      r,e
             10o0oo1rrrrrbooo   std     b,r
             100!001rrrrree-+   st      e,r
         */
    }
}
