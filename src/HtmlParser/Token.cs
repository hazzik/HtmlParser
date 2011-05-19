namespace HtmlParser
{
    using System.Diagnostics;

    [DebuggerDisplay("{Type}, {Value}")]
    internal class Token
    {
        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public TokenType Type { get; private set; }

        public string Value { get; private set; }

        public bool IsNotEmpty()
        {
            return string.IsNullOrEmpty(Value) == false;
        }
    }
}