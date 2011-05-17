namespace HtmlParser
{
    using System.Diagnostics;
    using System.Text;

    [DebuggerDisplay("{builder}")]
    internal class TokenBuilder
    {
        private readonly StringBuilder builder = new StringBuilder();

        private readonly TokenType type;

        public TokenBuilder(TokenType type)
        {
            this.type = type;
        }

        public void Append(string value)
        {
            builder.Append(value);
        }

        public void Append(char value)
        {
            builder.Append(value);
        }

        public Token Create()
        {
            return new Token(type, builder.ToString());
        }
    }
}