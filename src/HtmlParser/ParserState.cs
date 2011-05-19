namespace HtmlParser
{
	using System.Collections.Generic;

	internal abstract class ParserState
	{
		protected abstract bool HandleCore(Context context, ICollection<Token> tokens, char ch);

		public void Handle(Context context, ICollection<Token> tokens, char ch)
		{
			if (HandleCore(context, tokens, ch) == false)
			{
				context.TokenBuilder.Append(ch);
			}
		}
	}
}