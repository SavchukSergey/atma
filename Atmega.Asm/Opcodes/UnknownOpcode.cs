using System;
using System.Linq;

namespace Atmega.Asm.Opcodes {
    public class UnknownOpcode : BaseOpcode {

        private const string _map = @"
     1001010010001000   clc
     1001010011011000   clh
     1001010011111000   cli
     1001010010101000   cln
     1001010011001000   cls
     1001010011101000   clt
     1001010010111000   clv
     1001010010011000   clz
     1001010000001000   sec
     1001010001011000   seh
     1001010001111000   sei
     1001010000101000   sen
     1001010001001000   ses
     1001010001101000   set
     1001010000111000   sev
     1001010000011000   sez
     100101001SSS1000   bclr    S
     100101000SSS1000   bset    S
     1001010100001001   icall
     1001010000001001   ijmp
     1001010111001000   lpm     ?
     1001000ddddd010+   lpm     r,z
     1001010111011000   elpm    ?
     1001000ddddd011+   elpm    r,z
     0000000000000000   nop
     1001010100001000   ret
     1001010100011000   reti
     1001010110001000   sleep
     1001010110011000   break
     1001010110101000   wdr
     1001010111101000   spm
     000111rdddddrrrr   adc     r,r
     000011rdddddrrrr   add     r,r
     001000rdddddrrrr   and     r,r
     000101rdddddrrrr   cp      r,r
     000001rdddddrrrr   cpc     r,r
     000100rdddddrrrr   cpse    r,r
     001001rdddddrrrr   eor     r,r
     001011rdddddrrrr   mov     r,r
     100111rdddddrrrr   mul     r,r
     001010rdddddrrrr   or      r,r
     000010rdddddrrrr   sbc     r,r
     000110rdddddrrrr   sub     r,r
     001001rdddddrrrr   clr     r
     000011rdddddrrrr   lsl     r
     000111rdddddrrrr   rol     r
     001000rdddddrrrr   tst     r
     0111KKKKddddKKKK   andi    d,M
     0111KKKKddddKKKK   cbr     d,n
     1110KKKKddddKKKK   ldi     d,M
     11101111dddd1111   ser     d
     0110KKKKddddKKKK   ori     d,M
     0110KKKKddddKKKK   sbr     d,M
     0011KKKKddddKKKK   cpi     d,M
     0100KKKKddddKKKK   sbci    d,M
     0101KKKKddddKKKK   subi    d,M
     1111110rrrrr0sss   sbrc    r,s
     1111111rrrrr0sss   sbrs    r,s
     1111100ddddd0sss   bld     r,s
     1111101ddddd0sss   bst     r,s
     10110PPdddddPPPP   in      r,P
     10111PPrrrrrPPPP   out     P,r
     10010110KKddKKKK   adiw    w,K
     10010111KKddKKKK   sbiw    w,K
     10011000pppppsss   cbi     p,s
     10011010pppppsss   sbi     p,s
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
     1001010rrrrr0101   asr     r
     1001010rrrrr0000   com     r
     1001010rrrrr1010   dec     r
     1001010rrrrr0011   inc     r
     1001010rrrrr0110   lsr     r
     1001010rrrrr0001   neg     r
     1001000rrrrr1111   pop     r
     1001001rrrrr1111   push    r
     1001010rrrrr0111   ror     r
     1001010rrrrr0010   swap    r
     00000001ddddrrrr   movw    v,v
     00000010ddddrrrr   muls    d,d
     000000110ddd0rrr   mulsu   a,a
     000000110ddd1rrr   fmul    a,a
     000000111ddd0rrr   fmuls   a,a
     000000111ddd1rrr   fmulsu  a,a
     1001001ddddd0000   sts     i,r
     1001000ddddd0000   lds     r,i
     10o0oo0dddddbooo   ldd     r,b
     1001000dddddee-+   ld      r,e
     10o0oo1rrrrrbooo   std     b,r
     1001001rrrrree-+   st      e,r
     1001010100011001   eicall
     1001010000011001   eijmp";

        public UnknownOpcode()
            : base("0000000000000000") {
        }

        public override void Compile(AsmParser parser, AsmSection output) {
            throw new NotImplementedException();
        }

        public ushort Opcode { get; set; }

        public override string ToString() {
            var items = _map
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(item => item.Trim())
                .Where(item => !string.IsNullOrWhiteSpace(item))
                .ToArray();
            string found = null;
            foreach (var item in items) {
                if (MatchItem(item)) {
                    found = item;
                    break;
                }
            }
            return string.Format("unknown 0x{0:x4} ;{1}", Opcode, found);
        }

        private bool MatchItem(string item) {
            item = item.Trim();
            var code = Opcode;
            for (var i = 15; i >= 0; i--) {
                var bt = item[15 - i];
                var mask = 1 << i;
                switch (bt) {
                    case '0':
                        if ((code & mask) != 0) {
                            return false;
                        }
                        break;
                    case '1':
                        if ((code & mask) == 0) {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }
    }
}
