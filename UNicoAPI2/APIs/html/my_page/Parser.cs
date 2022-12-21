using System.Collections.Generic;
using System.Text;

namespace UNicoAPI2.APIs.html.my_page
{
    public class Parser : IHtmlParser<Dictionary<string, string>>
    {
        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public Dictionary<string, string> Parse(string Value)
        {
            return new user_page.Parser().Parse(Value);
        }
    }
}
