using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.user_mylist_page_html
{
    public class Parser : IHtmlParser<Dictionary<string, string>[]>
    {
        static readonly Regex regex = new Regex("<a href=\"mylist\\/(?<id>.*?)\"><span class=\"(?<value>.*?)\"></span>(?<name>.*?)</a>");

        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public Dictionary<string, string>[] Parse(string Value)
        {
            var result = new List<Dictionary<string, string>>();

            foreach (Match item in regex.Matches(Value))
            {
                var info = new Dictionary<string, string>();

                result.Add(info);
                info["id"] = item.Groups["id"].Value;
                info["name"] = item.Groups["name"].Value;
            }

            return result.ToArray();
        }
    }
}
