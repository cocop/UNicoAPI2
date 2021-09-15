using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.search_page_html
{
    public class Parser : IHtmlParser<Dictionary<string, string>[]>
    {
        static readonly Regex videoInfoRegex = new Regex("<p class=\"itemTime\">.*?<span class=\"time\">[ \n]*?(?<time>.*?)[ \n]*?</span>.*?</p>.*?data-id=\"(?<id>.*?)\">.*?<img .*?data-original=\"(?<thumbnail>.*?)\".*?>.*?<span class=\"videoLength\">(?<length>.*?)</span>.*?<p class=\"itemTitle\">.*?<a.*?title=\"(?<title>.*?)\".*?>.*?</a>.*?<p.*?>(?<short_desc>.*?)</p>.*?<li class=\"count view\">.*?<span class=\"value\">(?<view>.*?)</span>.*?</li>.*?<li class=\"count comment\">.*?<span class=\"value\">(?<comment>.*?)</span>.*?</li>.*?<li class=\"count like\">.*?<span class=\"value\">(?<like>.*?)</span>.*?</li>.*?<li class=\"count mylist\">.*?<span class=\"value\">(?<mylist>.*?)</span>.*?</li>", RegexOptions.Singleline);

        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public Dictionary<string, string>[] Parse(string Value)
        {
            var result = new List<Dictionary<string, string>>();
            foreach (Match item in videoInfoRegex.Matches(Value))
            {
                var info = new Dictionary<string, string>();

                info["id"] = item.Groups["id"].Value;
                info["title"] = item.Groups["title"].Value;
                info["short_desc"] = item.Groups["short_desc"].Value;
                info["length"] = item.Groups["length"].Value;
                info["view"] = item.Groups["view"].Value.Replace(",", "");
                info["comment"] = item.Groups["comment"].Value.Replace(",", "");
                info["like"] = item.Groups["like"].Value.Replace(",", "");
                info["mylist"] = item.Groups["mylist"].Value.Replace(",", "");

                var thumbnailUrl = item.Groups["thumbnail"].Value;
                if (thumbnailUrl[thumbnailUrl.Length - 2] == '.')
                    thumbnailUrl = thumbnailUrl.Substring(0, thumbnailUrl.Length - 2);

                info["thumbnail"] = thumbnailUrl;

                result.Add(info);
            }

            return result.ToArray();
        }
    }
}
