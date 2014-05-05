using System.Collections.Generic;
using Atmega.Asm.Hex;
using Atmega.Asm.Tokens;

namespace Atmega.Asm {
    public class AsmContext {
        private readonly AsmParser _parser;

        public int Pass { get; set; }

        public TokensQueue Queue { get; set; }

        public AsmSymbols Symbols { get; set; }

        private readonly AsmSection _codeSection = new AsmSection();
        private readonly AsmSection _dataSection = new AsmSection();
        private readonly AsmSection _flashSection = new AsmSection();
        private AsmSectionType _currentSection;

        public AsmContext() {
            _parser = new AsmParser(this);

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

        public AsmParser Parser {
            get { return _parser; }
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

        public HexFile BuildHexFile() {
            var hexFile = new HexFile();
            CodeSection.WriteTo(hexFile);
            hexFile.Lines.Add(new HexFileLine { Type = HexFileLineType.Eof });
            return hexFile;
        }

        private readonly IDictionary<string, ushort> _passLabels = new Dictionary<string, ushort>();

        public string LastGlobalLabel { get; set; }

        private bool LabelDefined(Token token) {
            var fullname = GetFullLabelName(token);
            return _passLabels.ContainsKey(fullname);
        }

        public void DefineLabel(Token token) {
            if (LabelDefined(token)) {
                throw new TokenException("duplicate label", token);
            }
            if (!token.StringValue.StartsWith(".")) {
                LastGlobalLabel = token.StringValue;
            }
            var fullname = GetFullLabelName(token);
            _passLabels[fullname] = (ushort)Offset;
            Symbols.Labels[fullname] = (ushort)Offset;
        }

        public string GetFullLabelName(Token token) {
            var name = token.StringValue;
            if (name.StartsWith(".")) {
                if (string.IsNullOrWhiteSpace(LastGlobalLabel)) {
                    throw new TokenException("local label must be preceded by global name", token);
                }
                return LastGlobalLabel + name;
            }
            return name;
        }

        public ushort? GetLabel(Token token) {
            var fullname = GetFullLabelName(token);
            ushort val;
            if (_passLabels.TryGetValue(fullname, out val)) {
                return val;
            }
            if (Symbols.Labels.TryGetValue(fullname, out val)) {
                return val;
            }
            if (Pass <= 1) return 0;
            return null;
        }

    }
}
