using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.user_page_html
{
    public class Parser : IHtmlParser<Dictionary<string, string>>
    {
        static readonly Regex jsonRegex = new Regex("data-environment=\"(?<json>.*?)\"");

        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public Dictionary<string, string> Parse(string Value)
        {
            var matchedJsonText = jsonRegex.Match(Value).Groups["json"];
            if (matchedJsonText == null || matchedJsonText.Value == "")
                return null;

            var jsonBinary = Encoding.UTF8.GetBytes(WebUtility.HtmlDecode(matchedJsonText.Value));
            var data = (Response.Rootobject)JsonSerializer.Deserialize(jsonBinary, typeof(Response.Rootobject));

            var result = new Dictionary<string, string>();
            result["icon"] = data.viewer.icons.large;
            result["name"] = data.viewer.nickname;
            result["id"] = data.userId.ToString();
            result["category"] = data.viewer.isPremium ? "プレミアム会員" : "一般会員";
            result["description"] = data.viewer.description;

            return result;
        }
    }
}
