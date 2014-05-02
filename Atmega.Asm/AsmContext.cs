﻿using System;
using System.Collections.Generic;
using Atmega.Asm.Tokens;

namespace Atmega.Asm {
    public class AsmContext {

        private readonly IList<byte> _code = new List<byte>();

        public TokensQueue Queue { get; set; }

        public int CodeOffset { get; set; }

        public IList<byte> Code {
            get { return _code; }
        }

        public void EmitCode(ushort opcode) {
            AlignCode(2);
            _code.Add((byte)(opcode & 0xff));
            _code.Add((byte)((opcode >> 8) & 0xff));
            CodeOffset += 2;
        }

        public void ReserveByte() {
            //TODO: alignment should take any space if put at the end of section. put uninitialized pointer
            Code.Add(0);
            CodeOffset++;
        }

        public void AlignCode(byte alignment) {
            switch (alignment) {
                case 2:
                    var mask = ~(alignment - 1);
                    var targetOffset = (CodeOffset + 1) & mask;
                    while (CodeOffset < targetOffset) {
                        ReserveByte();
                    }
                    break;
                default:
                    throw new Exception("Invalid alignment");
            }
        }

        public byte ReadReg32() {
            var reg = ParseReg();
            return reg;
        }

        public byte ReadReg16() {
            var reg = ParseReg();
            if (reg < 16) {
                throw new Exception("Expected r16-r31");
            }
            return reg;
        }

        public byte ReadByte() {
            var val = CalculateExpression();
            if (val < 0) {
                val = 256 + val;
            }
            if (val > 255) {
                throw new Exception("Value is out of range");
            }

            return (byte)val;
        }

        public byte ReadPort32() {
            var val = CalculateExpression();
            if (val < 0 || val > 32) {
                throw new Exception("Expected port address 0-31");
            }
            return (byte)val;
        }

        public byte ReadPort64() {
            var val = CalculateExpression();
            if (val < 0 || val > 64) {
                throw new Exception("Expected port address 0-63");
            }
            return (byte)val;
        }

        public byte ReadBit() {
            var val = CalculateExpression();
            if (val < 0 || val > 7) {
                throw new Exception("Expected bit number 0-7");
            }
            return (byte)val;
        }

        private long CalculateExpression() {
            //TODO: eof and eol check
            var token = Queue.Read(TokenType.Integer);
            return token.IntegerValue;
        }

        private byte ParseReg() {
            //TODO: eof and eol check
            var token = Queue.Read(TokenType.Literal);
            switch (token.StringValue.ToLower()) {
                case "r0": return 0;
                case "r1": return 1;
                case "r2": return 2;
                case "r3": return 3;
                case "r4": return 4;
                case "r5": return 5;
                case "r6": return 6;
                case "r7": return 7;
                case "r8": return 8;
                case "r9": return 9;
                case "r10": return 10;
                case "r11": return 11;
                case "r12": return 12;
                case "r13": return 13;
                case "r14": return 14;
                case "r15": return 15;
                case "r16": return 16;
                case "r17": return 17;
                case "r18": return 18;
                case "r19": return 19;
                case "r20": return 20;
                case "r21": return 21;
                case "r22": return 22;
                case "r23": return 23;
                case "r24": return 24;
                case "r25": return 25;
                case "r26": return 26;
                case "r27": return 27;
                case "r28": return 28;
                case "r29": return 29;
                case "r30": return 30;
                case "r31": return 31;
                default:
                    throw new TokenException("Register expected", token);
            }
        }

    }
}
