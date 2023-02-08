using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.html.user_mylist_page
{
    public class Parser : HtmlParser<Dictionary<string, string>[]>
    {
        static readonly Regex regex = new Regex("<a href=\"mylist\\/(?<id>.*?)\"><span class=\"(?<value>.*?)\"></span>(?<name>.*?)</a>");

        public override Dictionary<string, string>[] Parse(string Value)
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
