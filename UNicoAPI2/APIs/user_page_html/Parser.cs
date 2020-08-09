using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.user_page_html
{
    public class Parser : IHtmlParser<Dictionary<string, string>>
    {
        static readonly Regex jsonRegex = new Regex("data-initial-data=\"(?<json>.*?)\"");

        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public Dictionary<string, string> Parse(string Value)
        {
            var matchedJsonText = jsonRegex.Match(Value).Groups["json"];
            var jsonBinary = Encoding.UTF8.GetBytes(WebUtility.HtmlDecode(matchedJsonText.Value));
            var serialize = new DataContractJsonSerializer(typeof(Serial.Rootobject));
            var data = (Serial.Rootobject)serialize.ReadObject(new MemoryStream(jsonBinary));

            var result = new Dictionary<string, string>();
            result["icon"] = data.userDetails.userDetails.user.icons.large;
            result["name"] = data.userDetails.userDetails.user.nickname;
            result["id"] = data.userDetails.userDetails.user.id;
            result["category"] = data.userDetails.userDetails.user.premiumTicketExpireTime == null ? "一般会員" : "プレミアム会員";
            result["bookmark"] = data.userDetails.userDetails.user.followeeCount.ToString();
            result["exp"] = data.userDetails.userDetails.user.userLevel.nextLevelExperience.ToString();
            result["description"] = data.userDetails.userDetails.user.description;

            return result;
        }
    }
}
