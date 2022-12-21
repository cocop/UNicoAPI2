using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.html.video_page
{
    public class Parser : IHtmlParser<Response.Rootobject>
    {
        static readonly Regex info =
            new Regex("<div id=\"js-initial-watch-data\" data-api-data=\"(?<value>.*?)\" data-environment=\"");

        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public Response.Rootobject Parse(string Value)
        {
            var result = info.Match(Value).Groups["value"].Value;
            result = WebUtility.HtmlDecode(result);

            var serialize = new DataContractJsonSerializer(typeof(Response.Rootobject));
            return (Response.Rootobject)serialize.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(result)));
        }
    }
}
