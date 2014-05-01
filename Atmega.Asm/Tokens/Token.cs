namespace Atmega.Asm.Tokens {
    public struct Token {

        public TokenPosition Position { get; set; }

        public TokenType Type { get; set; }

        public string StringValue { get; set; }
        public long IntegerValue { get; set; }
    }

    public enum TokenType {
        None,
        String,
        Integer,
        Literal,
        Punctuation,
        NewLine,
        Comma
    }
}
