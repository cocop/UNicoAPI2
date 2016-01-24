using System;
using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.video_page_html
{
    public class watch_auth_key : IHtmlParser<string>
    {
        static readonly Regex watchAuthKey =
            new Regex("watchAuthKey&quot;:&quot;(?<value>[0-9|a-f|:].*?)&quot;,&quot;");


        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public string Parse(string Value)
        {
            return watchAuthKey.Match(Value).Groups["value"].Value;
        }
    }
}
