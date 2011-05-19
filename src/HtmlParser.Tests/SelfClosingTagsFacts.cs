namespace HtmlParser.Tests
{
	using System.Linq;
	using Xunit;

	public class SelfClosingTagsFacts
	{
		[Fact]
		public void Br()
		{
			const string br = "<br/>";
			var tokens = new TokenParser().Parse(br).Single();
			Assert.Equal("br", tokens.Value);
		}
		[Fact]
		public void BrWithSpace()
		{
			const string br = "<br />";
			var tokens = new TokenParser().Parse(br).Single();
			Assert.Equal("br", tokens.Value);
		}
	}
}