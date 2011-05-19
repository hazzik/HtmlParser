namespace HtmlParser
{
	using System.Collections.Generic;

	internal class CDataHandler : ParserState
	{
		protected override bool HandleCore(Context context, ICollection<Token> tokens, char ch)
		{
			return false;
		}
	}
}