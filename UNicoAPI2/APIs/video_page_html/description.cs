using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.video_page_html
{
    public class html_video_info : IHtmlParser<string>
    {
        static readonly Regex info =
            new Regex("<div id=\"js-initial-watch-data\" data-api-data=\"(?<value>.*?)\" data-environment=\"");

        static readonly Regex description =
            new Regex("<p class=\"videoDescription description\">(?<value>.*?)</p>");

        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public string Parse(string Value)
        {
            var result = info.Match(Value).Groups["value"].Value;

            result = WebUtility.HtmlDecode(result);


            var serialize = new DataContractJsonSerializer(typeof(Serial.Rootobject));
            var root = (Serial.Rootobject)serialize.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(result)));

            return Regex.Unescape(root.video.description);
        }
    }
}
