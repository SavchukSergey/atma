using Atmega.Asm.Opcodes.Arithmetics;
using Atmega.Asm.Opcodes.Bit;
using Atmega.Asm.Opcodes.Branch;
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
                case "swap": return new SwapOpcode();
                case "push": return new PushOpcode();
                case "pop": return new PopOpcode();

                case "sbrc": return new SbrcOpcode();
                case "sbrs": return new SbrsOpcode();
                case "ijmp": return new IjmpOpcode();
                case "icall": return new IcallOpcode();
                case "eijmp": return new EijmpOpcode();
                case "eicall": return new EicallOpcode();
                case "ret": return new RetOpcode();
                case "reti": return new RetiOpcode();

                default: return null;
            }
        }

        /*
             100101001SSS1000   bclr    S
             100101000SSS1000   bset    S
             1001010111001000   lpm     ?
             1001000ddddd010+   lpm     r,z
             1001010111011000   elpm    ?
             1001000ddddd011+   elpm    r,z
             0000000000000000   nop
             1001010110001000   sleep
             1001010110011000   break
             1001010110101000   wdr
             1001010111101000   spm

             000100rdddddrrrr   cpse    r,r
             10110PPdddddPPPP   in      r,P
             10111PPrrrrrPPPP   out     P,r
             10010110KKddKKKK   adiw    w,K
             10010111KKddKKKK   sbiw    w,K
             10011001pppppsss   sbic    p,s
             10011011pppppsss   sbis    p,s
             111101lllllll000   brcc    l
             111100lllllll000   brcs    l
             111100lllllll001   breq    l
             111101lllllll100   brge    l
             111101lllllll101   brhc    l
             111100lllllll101   brhs    l
             111101lllllll111   brid    l
             111100lllllll111   brie    l
             111100lllllll000   brlo    l
             111100lllllll100   brlt    l
             111100lllllll010   brmi    l
             111101lllllll001   brne    l
             111101lllllll010   brpl    l
             111101lllllll000   brsh    l
             111101lllllll110   brtc    l
             111100lllllll110   brts    l
             111101lllllll011   brvc    l
             111100lllllll011   brvs    l
             111101lllllllsss   brbc    s,l
             111100lllllllsss   brbs    s,l
             1101LLLLLLLLLLLL   rcall   L
             1100LLLLLLLLLLLL   rjmp    L
             1001010hhhhh111h   call    h
             1001010hhhhh110h   jmp     h
             00000001ddddrrrr   movw    v,v
             00000010ddddrrrr   muls    d,d
             000000110ddd0rrr   mulsu   a,a
             000000110ddd1rrr   fmul    a,a
             000000111ddd0rrr   fmuls   a,a
             000000111ddd1rrr   fmulsu  a,a
             1001001ddddd0000   sts     i,r
             1001000ddddd0000   lds     r,i
             10o0oo0dddddbooo   ldd     r,b
             100!000dddddee-+   ld      r,e
             10o0oo1rrrrrbooo   std     b,r
             100!001rrrrree-+   st      e,r
         */
    }
}
