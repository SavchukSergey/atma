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

        public void EmitWord(ushort val) {
            CurrentSection.EmitWord(val);
        }

        public byte ReadRegW24() {
            var reg = ReadRegister();
            if (reg != 24 && reg != 26 && reg != 28 && reg != 30) {
                throw new TokenException("word register expected (r24, r26, r28, r30)", Queue.LastReadToken);
            }
            return reg;
        }

        public byte ReadReg32() {
            return ReadRegister();
        }

        public byte ReadReg16() {
            var reg = ReadRegister();
            if (reg < 16) {
                throw new TokenException("expected r16-r31", Queue.LastReadToken);
            }
            return reg;
        }

        public byte ReadReg8() {
            var reg = ReadRegister();
            if (reg < 16 || reg > 23) {
                throw new TokenException("expected r16-r23", Queue.LastReadToken);
            }
            return reg;
        }

        private byte ReadRegister() {
            if (Queue.IsEndOfLine) {
                throw new TokenException("register expected", Queue.LastReadToken);
            }
            var token = Queue.Read();
            if (token.Type != TokenType.Literal) {
                throw new TokenException("register expected", token);
            }
            return token.ParseRegister();
        }

        public byte ReadPort32() {
            Token firstToken;
            var val = CalculateExpression(out firstToken);
            if (val < 0 || val >= 32) {
                throw new TokenException("expected port address 0-31", firstToken);
            }
            return (byte)val;
        }

        public byte ReadPort64() {
            Token firstToken;
            var val = CalculateExpression(out firstToken);
            if (val < 0 || val >= 64) {
                throw new TokenException("expected port address 0-63", firstToken);
            }
            return (byte)val;
        }

        public byte ReadByte() {
            Token firstToken;
            var val = CalculateExpression(out firstToken);
            if (val < 0) {
                val = 256 + val;
            }
            if (val > 255) {
                throw new TokenException("byte value is out of range", firstToken);
            }

            return (byte)val;
        }

        public ushort ReadUshort() {
            Token firstToken;
            var val = CalculateExpression(out firstToken);
            if (val < 0 || val >= 0x10000) {
                throw new TokenException("address is beyond 64k boundary", firstToken);
            }
            return (ushort)val;
        }

        public byte ReadBit() {
            Token firstToken;
            var val = CalculateExpression(out firstToken);
            if (val < 0 || val > 7) {
                throw new TokenException("expected bit number 0-7", firstToken);
            }
            return (byte)val;
        }

        public long CalculateExpression(out Token firstToken) {
            if (Queue.IsEndOfLine) {
                throw new TokenException("expression expected", Queue.LastReadToken);
            }
            firstToken = Queue.Peek();
            return _calculator.Parse(Queue).Evaluate();
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
