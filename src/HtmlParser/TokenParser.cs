namespace HtmlParser
{
	using System.Collections.Generic;
	using System.Linq;

	internal class TokenParser
	{
		private ParserState handler;

		public TokenParser()
		{
			TokenBuilder = new TokenBuilder(TokenType.Text);
			handler = new TextHandler();
		}

		public TokenBuilder TokenBuilder { get; private set; }

		public Token SwitchState(TokenType tokenType, ParserState parserState)
		{
			Token token = TokenBuilder.Create();
			handler = parserState;
			TokenBuilder = new TokenBuilder(tokenType);
			return token;
		}

		public IEnumerable<Token> Parse(string html)
		{
			var tokens = new List<Token>();
			
			foreach (char ch in html)
			{
				handler.Handle(this, tokens, ch);
			}

			tokens.Add(SwitchState(TokenType.Text, new TextHandler()));
			return tokens.Where(x => x.IsNotEmpty());
		}
	}
}