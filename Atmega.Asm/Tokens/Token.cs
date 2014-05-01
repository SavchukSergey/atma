namespace Atmega.Asm.Tokens {
    public struct Token {

        public TokenPosition Position { get; set; }

        public TokenType Type { get; set; }

        public string StringValue { get; set; }

    }

    public enum TokenType {
        String,
        Integer,
        Literal,
        Punctuation,
        NewLine
    }
}
