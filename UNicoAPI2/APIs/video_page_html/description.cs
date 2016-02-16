using System;
using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.video_page_html
{
    public class html_video_info : IHtmlParser<string>
    {
        static readonly Regex description =
            new Regex("<p class=\"videoDescription description\">(?<value>.*?)</p>");

        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public string Parse(string Value)
        {
            return description.Match(Value).Groups["value"].Value;
        }
    }
}
