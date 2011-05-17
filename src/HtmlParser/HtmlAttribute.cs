namespace HtmlParser
{
    using System.Diagnostics;

    [DebuggerDisplay("{Name} = {Value}")]
    public class HtmlAttribute
    {
        internal HtmlAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public string Value { get; set; }
    }
}