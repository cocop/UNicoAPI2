using System.Collections.Generic;
using System.Text;

namespace UNicoAPI2.APIs.my_page_html
{
    public class Parser : IHtmlParser<Dictionary<string, string>>
    {
        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public Dictionary<string, string> Parse(string Value)
        {
            return new user_page_html.Parser().Parse(Value);
        }
    }
}
