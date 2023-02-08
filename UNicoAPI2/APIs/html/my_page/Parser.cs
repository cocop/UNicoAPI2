using System.Collections.Generic;
using System.Text;

namespace UNicoAPI2.APIs.html.my_page
{
    public class Parser : HtmlParser<Dictionary<string, string>>
    {
        public override Dictionary<string, string> Parse(string Value)
        {
            return new user_page.Parser().Parse(Value);
        }
    }
}
