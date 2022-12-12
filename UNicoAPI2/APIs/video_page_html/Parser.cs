using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.video_page_html
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

            return (Response.Rootobject)JsonSerializer.Deserialize(result, typeof(Response.Rootobject));
        }
    }
}
