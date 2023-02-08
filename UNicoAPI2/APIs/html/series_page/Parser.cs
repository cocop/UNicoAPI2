using AngleSharp.Dom;
using AngleSharp.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;


namespace UNicoAPI2.APIs.html.series_page
{
    public class Parser : HtmlParser<Response.Rootobject>
    {
        public override Response.Rootobject Parse(string Value)
        {
            var htmlParser = new AngleSharp.Html.Parser.HtmlParser();
            var htmlDocuments = htmlParser.ParseDocument(Value);
            var injson = htmlDocuments
                .QuerySelector("#js-initial-userpage-data")
                .GetAttribute("data-initial-data");

            var serialize = new DataContractJsonSerializer(typeof(Response.Rootobject));
            return (Response.Rootobject)serialize.ReadObject(new MemoryStream(TextEncoding.Utf8.GetBytes(HttpUtility.HtmlDecode(injson))));
        }
    }
}