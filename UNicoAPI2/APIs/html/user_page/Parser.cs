using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.html.user_page
{
    public class Parser : HtmlParser<Dictionary<string, string>>
    {
        static readonly Regex jsonRegex = new Regex("data-environment=\"(?<json>.*?)\"");

        public override Dictionary<string, string> Parse(string Value)
        {
            var matchedJsonText = jsonRegex.Match(Value).Groups["json"];
            if (matchedJsonText == null || matchedJsonText.Value == "")
                return null;

            var jsonBinary = Encoding.UTF8.GetBytes(WebUtility.HtmlDecode(matchedJsonText.Value));
            var serialize = new DataContractJsonSerializer(typeof(Response.Rootobject));
            var data = (Response.Rootobject)serialize.ReadObject(new MemoryStream(jsonBinary));

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
