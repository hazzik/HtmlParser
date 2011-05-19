namespace HtmlParser
{
	using System.Collections.Generic;

	internal class Context
	{
		private ParserState handler;

		public Context()
		{
			TokenBuilder = new TokenBuilder(TokenType.Text);
			handler = new TextHandler();
		}

		public TokenBuilder TokenBuilder { get; private set; }

		public Token SwitchState(TokenType tokenType, ParserState parserState)
		{
			SetState(parserState);
			Token token = TokenBuilder.Create();
			TokenBuilder = new TokenBuilder(tokenType);
			return token;
		}

		public void SetState(ParserState parserState)
		{
			handler = parserState;
		}

		public void Handle(ICollection<Token> tokens, char ch)
		{
			handler.Handle(this, tokens, ch);
		}
	}
}