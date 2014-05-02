using System;
using System.Collections.Generic;
using Atmega.Asm.Expressions;
using Atmega.Asm.Tokens;

namespace Atmega.Asm {
    public class AsmContext {

        private readonly IList<byte> _code = new List<byte>();

        public int Pass { get; set; }

        public TokensQueue Queue { get; set; }

        public AsmSymbols Symbols { get; set; }

        public int CodeOffset { get; set; }

        public int Offset {
            get { return CodeOffset; }
            set { CodeOffset = value; }
        }

        public IList<byte> Code {
            get { return _code; }
        }

        public void EmitCode(ushort opcode) {
            AlignCode(2);
            _code.Add((byte)(opcode & 0xff));
            _code.Add((byte)((opcode >> 8) & 0xff));
            CodeOffset += 2;
        }

        public void EmitByte(byte bt) {
            _code.Add(bt);
            Offset++;
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

        public Token ReadRequiredToken() {
            if (Queue.Count == 0) {
                throw new Exception("Unexpected end of file");
            }
            var token = Queue.Read();
            if (token.Type == TokenType.NewLine) {
                throw new Exception("Unexpected end of line");
            }
            return token;
        }

        public Token PeekRequiredToken() {
            if (Queue.Count == 0) {
                throw new Exception("Unexpected end of file");
            }
            var token = Queue.Peek();
            if (token.Type == TokenType.NewLine) {
                throw new Exception("Unexpected end of line");
            }
            return token;
        }

        public byte ReadReg32() {
            var token = ReadRequiredToken();
            if (token.Type != TokenType.Literal) {
                throw new TokenException("register expected", token);
            }
            var reg = token.ParseRegister();
            return reg;
        }

        public byte ReadReg16() {
            var token = ReadRequiredToken();
            if (token.Type != TokenType.Literal) {
                throw new TokenException("register expected", token);
            }
            var reg = token.ParseRegister();
            if (reg < 16) {
                throw new TokenException("Expected r16-r31", token);
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
            var token = PeekRequiredToken();
            var val = CalculateExpression();
            if (val < 0 || val > 32) {
                throw new TokenException("expected port address 0-31", token);
            }
            return (byte)val;
        }

        public byte ReadPort64() {
            var token = PeekRequiredToken();
            var val = CalculateExpression();
            if (val < 0 || val > 64) {
                throw new TokenException("expected port address 0-63", token);
            }
            return (byte)val;
        }

        public ushort ReadUshort() {
            var val = CalculateExpression();
            if (val < 0 || val > 0x10000) {
                throw new Exception("addres is beyound 64k boundary");
            }
            return (ushort)val;
        }

        public byte ReadBit() {
            var val = CalculateExpression();
            if (val < 0 || val > 7) {
                throw new Exception("Expected bit number 0-7");
            }
            return (byte)val;
        }

        private readonly ExpressionCalculator _calculator = new ExpressionCalculator();

        public long CalculateExpression() {
            return _calculator.Calculate(this);
        }



        private readonly IDictionary<string, ushort> _passLabels = new Dictionary<string, ushort>();

        public bool LabelDefined(string name) {
            return _passLabels.ContainsKey(name);
        }

        public void DefineLabel(string name) {
            _passLabels[name] = (ushort)Offset;
            Symbols.Labels[name] = (ushort)Offset;
        }

        public ushort? GetLabel(string name) {
            ushort val;
            if (_passLabels.TryGetValue(name, out val)) {
                return val;
            }
            if (Symbols.Labels.TryGetValue(name, out val)) {
                return val;
            }
            if (Pass <= 1) return 0;
            return null;
        }

    }
}
