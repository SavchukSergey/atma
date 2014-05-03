using System;
using System.Collections.Generic;
using Atmega.Asm.Expressions;
using Atmega.Asm.Hex;
using Atmega.Asm.Tokens;

namespace Atmega.Asm {
    public class AsmContext {

        public int Pass { get; set; }

        public TokensQueue Queue { get; set; }

        public AsmSymbols Symbols { get; set; }

        private readonly AsmSection _codeSection = new AsmSection();
        private readonly AsmSection _dataSection = new AsmSection();
        private readonly AsmSection _flashSection = new AsmSection();
        private AsmSectionType _currentSection;

        private readonly ExpressionCalculator _calculator;

        public AsmContext() {
            _calculator = new ExpressionCalculator(this);
        }

        public AsmSection CodeSection {
            get { return _codeSection; }
        }

        public AsmSection DataSection {
            get { return _dataSection; }
        }

        public AsmSection FlashSection {
            get { return _flashSection; }
        }

        public AsmSection CurrentSection {
            get {
                switch (_currentSection) {
                    case AsmSectionType.Code:
                        return _codeSection;
                    case AsmSectionType.Data:
                        return _dataSection;
                    case AsmSectionType.Flash:
                        return _flashSection;
                    default:
                        return _codeSection;
                }
            }
        }

        public void SetSection(AsmSectionType type) {
            _currentSection = type;
        }

        public int Offset {
            get { return CurrentSection.Offset; }
            set { CurrentSection.Offset = value; }
        }

        public void EmitCode(ushort opcode) {
            CurrentSection.EmitCode(opcode);
        }

        public void EmitByte(byte bt) {
            CurrentSection.EmitByte(bt);
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
                throw new Exception("address is beyond 64k boundary");
            }
            return (ushort)val;
        }

        public byte ReadBit() {
            var val = CalculateExpression();
            if (val < 0 || val > 7) {
                throw new Exception("expected bit number 0-7");
            }
            return (byte)val;
        }

        public long CalculateExpression() {
            return _calculator.Parse(Queue).Evaluate();
        }

        public HexFile BuildHexFile() {
            var hexFile = new HexFile();
            CodeSection.WriteTo(hexFile);
            hexFile.Lines.Add(new HexFileLine { Type = HexFileLineType.Eof });
            return hexFile;
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
