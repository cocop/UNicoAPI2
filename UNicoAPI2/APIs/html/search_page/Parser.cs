using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace UNicoAPI2.APIs.html.search_page
{
    public class Parser : HtmlParser<Dictionary<string, string>[]>
    {
        public override Dictionary<string, string>[] Parse(string Value)
        {
            var result = new List<Dictionary<string, string>>();
            var htmlParser = new AngleSharp.Html.Parser.HtmlParser();
            var htmlDocuments = htmlParser.ParseDocument(Value);

            foreach (var item in htmlDocuments.QuerySelectorAll(@"ul[class=""videoListInner""] > li"))
            {
                var thumbnailUrl = item.QuerySelector(@"img[class=""thumb""]")?.GetAttribute("src");
                if (thumbnailUrl != null && thumbnailUrl[thumbnailUrl.Length - 2] == '.')
                    thumbnailUrl = thumbnailUrl.Substring(0, thumbnailUrl.Length - 2);

                var itemDict = new Dictionary<string, string>
                {
                    ["id"] = item.GetAttribute("data-video-id"),
                    ["time"] = item.QuerySelector(".time")?.InnerHtml,
                    ["title"] = item.QuerySelector(".itemTitle > a")?.GetAttribute("title"),
                    ["short_desc"] = item.QuerySelector(".itemDescription")?.InnerHtml,
                    ["length"] = item.QuerySelector(".videoLength")?.InnerHtml,
                    ["view"] = item.QuerySelector(".count.view > .value")?.InnerHtml?.Replace(",", ""),
                    ["comment"] = item.QuerySelector(".count.comment > .value")?.InnerHtml?.Replace(",", ""),
                    ["like"] = item.QuerySelector(".count.like > .value")?.InnerHtml?.Replace(",", ""),
                    ["mylist"] = item.QuerySelector(".count.mylist > .value")?.InnerHtml?.Replace(",", ""),
                    ["thumbnail"] = thumbnailUrl
                };

                if (itemDict["length"] != null)
                {
                    result.Add(itemDict);
                }
            }

            return result.ToArray();
        }
    }
}
