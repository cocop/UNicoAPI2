using AngleSharp.Text;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web;


namespace UNicoAPI2.APIs.html.search_page
{
    public class Parser : HtmlParser<Response.Rootobject>
    {
        public override Response.Rootobject Parse(string Value)
        {
            var htmlParser = new AngleSharp.Html.Parser.HtmlParser();
            var htmlDocuments = htmlParser.ParseDocument(Value);
            var injson = htmlDocuments
                .QuerySelector("meta[name=\"server-response\"]")
                ?.GetAttribute("content");

            var serialize = new DataContractJsonSerializer(typeof(Response.Rootobject));
            return (Response.Rootobject)serialize.ReadObject(new MemoryStream(TextEncoding.Utf8.GetBytes(HttpUtility.HtmlDecode(injson))));
        }
    }
}
