using System;
using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.video_page_html
{
    public class csrf_token : IHtmlParser<string>
    {
        static readonly Regex csrfToken =
            new Regex("csrfToken&quot;:&quot;(?<value>[0-9|a-f|-].*?)&quot;,");


        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public string Parse(string Value)
        {
            return csrfToken.Match(Value).Groups["value"].Value;
        }
    }
}
