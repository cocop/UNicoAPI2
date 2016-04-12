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
            new Regex("\t\t\\<div class=\"account\"\\>\n\t\t\t\\<p class=\"accountNumber\"\\>ID:\\<span\\>(?<id>.*?)\\((?<version>.*?)\\) (?<category>.*?)\\</span\\>\\</p\\>\n\t\t\t\\<p\\>性別:\\<span\\>(?<sex>.*?)\\</span\\>\\</p\\>\n\t\t\t\\<p\\>生年月日:\\<span\\>(?<birthday>.*?)\\</span\\>\\</p\\>\n\t\t\t\\<p\\>お住まいの地域:\\<span\\>(?<area>.*?)\\</span\\>\\</p\\>\n\t\t\\</div\\>"),
            new Regex("\t\t\t\\<li class=\"fav\" title=\"お気に入り登録された数\"\\>\\<span\\>\\</span\\>(?<bookmark>.*?)\\</li>\n\t\t\t\\<li class=\"exp\" title=\"スタンプ経験値\"\\>\\<a href=\".*?\"\\>\\<span\\>\\</span\\>(?<exp>[0-9].*?)EXP\\</a\\>\\</li\\>"),
            new Regex("\t\t\t\t\\<p id=\"description_full\" style=\"display: none;\"\\>\n\t\t\t\t\t\\<span\\>(?<description>.*?)\\</span\\>\n\t\t\t\t\\</p\\>", RegexOptions.Singleline),
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
            result["sex"] = matched["sex"].Value;
            result["birthday"] = matched["birthday"].Value;
            result["area"] = matched["area"].Value;

            matched = infoRegexs[2].Match(Value).Groups;
            result["bookmark"] = matched["bookmark"].Value;
            result["exp"] = matched["exp"].Value;

            matched = infoRegexs[3].Match(Value).Groups;
            result["description"] = matched["description"].Value;

            return result;
        }
    }
}
