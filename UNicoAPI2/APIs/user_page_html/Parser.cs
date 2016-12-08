using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.user_page_html
{
    public class Parser : IHtmlParser<Dictionary<string, string>>
    {
        static readonly Regex[] infoRegexs =
        {
            new Regex("\\<div class=\"userDetail\"\\>\n\t\\<div class=\"avatar\"\\>\n\t\t\\<img src=\"(?<icon>.*?)\" alt=\"(?<name>.*?)\" />\n\t</div>"),
            new Regex("<p class=\"accountNumber\">ID:<span>(?<id>[0-9].*?)\\(.*?\\)(?<category>.*?)</span></p>"),
            new Regex("<li>フォロワー: <span class=\"num\">(?<bookmark>.*?)</span></li>"),
            new Regex("<li>スタンプ: <a href=\".*?\" title=\"スタンプ経験値\"><span class=\"num\">(?<exp>[0-9].*?)</span>EXP</a></li>"),
            new Regex("<p id=\"description_full\" style=\"display: none;\">.*?<span>(?<description>.*?)</span>.*?</p>", RegexOptions.Singleline),
        };

        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public Dictionary<string, string> Parse(string Value)
        {
            var result = new Dictionary<string, string>();
            GroupCollection matched = null;

            matched = infoRegexs[0].Match(Value).Groups;
            result["icon"] = matched["icon"].Value;
            result["name"] = matched["name"].Value;

            matched = infoRegexs[1].Match(Value).Groups;
            result["id"] = matched["id"].Value;
            result["category"] = matched["category"].Value;

            matched = infoRegexs[2].Match(Value).Groups;
            result["bookmark"] = matched["bookmark"].Value;

            matched = infoRegexs[3].Match(Value).Groups;
            result["exp"] = matched["exp"].Value;

            matched = infoRegexs[4].Match(Value).Groups;
            result["description"] = matched["description"].Value;

            return result;
        }
    }
}
