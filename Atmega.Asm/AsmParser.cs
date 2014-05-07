using Atmega.Asm.Expressions;
using Atmega.Asm.Opcodes;
using Atmega.Asm.Tokens;

namespace Atmega.Asm {
    public class AsmParser {
        private readonly AsmContext _context;
        private readonly ExpressionCalculator _calculator;

        public AsmParser(AsmContext context) {
            _context = context;
            _calculator = new ExpressionCalculator(context);
        }

        public byte ReadRegW24() {
            var reg = ReadRegister();
            if (reg != 24 && reg != 26 && reg != 28 && reg != 30) {
                throw new TokenException("word register expected (r24, r26, r28, r30)", _context.Queue.LastReadToken);
            }
            return reg;
        }

        public byte ReadReg32() {
            return ReadRegister();
        }

        public byte ReadReg16() {
            var reg = ReadRegister();
            if (reg < 16) {
                throw new TokenException("expected r16-r31", _context.Queue.LastReadToken);
            }
            return reg;
        }

        public byte ReadReg8() {
            var reg = ReadRegister();
            if (reg < 16 || reg > 23) {
                throw new TokenException("expected r16-r23", _context.Queue.LastReadToken);
            }
            return reg;
        }

        private byte ReadRegister() {
            if (_context.Queue.IsEndOfLine) {
                throw new TokenException("register expected", _context.Queue.LastReadToken);
            }
            var token = _context.Queue.Read();
            if (token.Type != TokenType.Literal) {
                throw new TokenException("register expected", token);
            }
            var res = token.ParseRegister();
            if (res >= 32) {
                throw new TokenException("register expected", token);
            }

            return res;
        }

        public IndirectRegister ReadIndirectReg() {
            var reg = _context.Queue.Read(TokenType.Literal);
            switch (reg.StringValue.ToLower()) {
                case "x": return IndirectRegister.X;
                case "y": return IndirectRegister.Y;
                case "z": return IndirectRegister.Z;
                default:
                    throw new TokenException("X, Y or Z register expected", reg);
            }
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

        public byte ReadDesRound() {
            Token firstToken;
            var val = CalculateExpression(out firstToken);
            if (val < 0 || val > 15) {
                throw new TokenException("DES round must be between 0 and 15", firstToken);
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

        public IndirectOperand ReadIndirectOperand() {
            var decrement = false;
            if (!_context.Queue.IsEndOfLine && _context.Queue.Peek().Type == TokenType.Minus) {
                decrement = true;
                _context.Queue.Read(TokenType.Minus);
            }

            var reg = ReadIndirectReg();

            var increment = false;
            if (!_context.Queue.IsEndOfLine && _context.Queue.Peek().Type == TokenType.Plus) {
                increment = true;
                _context.Queue.Read(TokenType.Plus);
            }

            if (increment && decrement) {
                throw new TokenException("Only pre-decrement or post-increment can be specified at one time", _context.Queue.LastReadToken);
            }

            return new IndirectOperand {
                Register = reg,
                Increment = increment,
                Decrement = decrement
            };
        }

        public IndirectOperandWithDisplacement ReadIndirectWithDisplacement() {
            IndirectOperandWithDisplacement res;
            var reg = _context.Queue.Read(TokenType.Literal);
            switch (reg.StringValue.ToLower()) {
                case "y":
                    res.Register = IndirectRegister.Y;
                    break;
                case "z":
                    res.Register = IndirectRegister.Z;
                    break;
                default:
                    throw new TokenException("Y or Z register expected", reg);
            }

            res.Displacement = 0;
            if (!_context.Queue.IsEndOfLine) {
                var preview = _context.Queue.Peek();
                if (preview.Type == TokenType.Plus) {
                    _context.Queue.Read(TokenType.Plus);
                    Token exprToken;
                    var displacement = CalculateExpression(out exprToken);
                    if (displacement < 0 || displacement > 63) {
                        throw new TokenException("displacement must be between 0 and 63", exprToken);
                    }
                    res.Displacement = (byte)displacement;
                }
            }
            return res;
        }

        public long CalculateExpression(out Token firstToken) {
            if (_context.Queue.IsEndOfLine) {
                throw new TokenException("expression expected", _context.Queue.LastReadToken);
            }
            firstToken = _context.Queue.Peek();
            return _calculator.Parse(_context.Queue).Evaluate();
        }

        public long CalculateExpression() {
            return _calculator.Parse(_context.Queue).Evaluate();
        }

        public Token ReadToken(TokenType type) {
            return _context.Queue.Read(type);
        }
    }
}
