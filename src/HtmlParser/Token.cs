using System;
using System.Diagnostics;
using System.Text;

namespace HtmlParser
{
    [DebuggerDisplay("{Builder}")]
    internal class Token
    {
        private readonly StringBuilder builder = new StringBuilder();

        public Token(TokenType type)
        {
            Type = type;
        }

        public TokenType Type { get; set; }

        public StringBuilder Builder
        {
            get { return builder; }
        }

        public bool IsNotEmpty()
        {
            return Builder.Length > 0;
        }

        public string Value
        {
            get { return Builder.ToString(); }
        }
    }
}