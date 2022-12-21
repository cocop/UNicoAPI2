using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.html.series_page
{
    public class Parser : IHtmlParser<Response.Result>
    {
        static readonly Regex titleRegex = new Regex("<div class=\"SeriesDetailContainer-bodyTitle\">(?<title>.*?)</div>");
        static readonly Regex videoInfoRegex = new Regex("<a[^<>]*?href=\".*?watch/(?<id>.*?)\".*?>.*?<[^<>]*?data-background-image=\"(?<thumbnail_url>.*?)([.]M)?\".*?aria-label=\"(?<title>.*?)\">.*?<[^<>]*?view[^<>]*?>(?<view_count>.*?)<[^<>]*?>.*?<[^<>]*?comment[^<>]*?>(?<comment_count>.*?)<[^<>]*?>.*?<[^<>]*?like[^<>]*?>(?<like_count>.*?)<[^<>]*?>.*?<[^<>]*?mylist[^<>]*?>(?<mylist_count>.*?)<[^<>]*?>", RegexOptions.Singleline);
        static readonly Regex otherSeriesRegex = new Regex("<a class=\"SeriesMenuContainer-seriesItemLink\"href=\"https://www.nicovideo.jp/series/(?<id>.*?)\">(?<title>.*?)</a>");
        static readonly Regex postUserInfoRegex = new Regex("<a class=\"SeriesAdditionalContainer-ownerName\" href=\"https://www.nicovideo.jp/user/(?<id>.*?)\">(?<name>.*?)</a>");

        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public Response.Result Parse(string Value)
        {
            var result = new Response.Result();

            result.Title = titleRegex.Match(Value).Groups["title"].Value;

            var videoList = new List<Response.Video>();
            foreach (Match item in videoInfoRegex.Matches(Value))
            {
                videoList.Add(new Response.Video()
                {
                    Title = item.Groups["title"].Value,
                    ID = item.Groups["id"].Value,
                    ThumbnailUrl = item.Groups["thumbnail_url"].Value,
                    ViewCount = item.Groups["view_count"].Value,
                    CommentCount = item.Groups["comment_count"].Value,
                    MylistCount = item.Groups["mylist_count"].Value,
                });
            }
            result.VideoList = videoList.ToArray();

            var otherSeriesList = new List<Response.Series>();
            foreach (Match item in otherSeriesRegex.Matches(Value))
            {
                otherSeriesList.Add(new Response.Series()
                {
                    ID = item.Groups["id"].Value,
                    Title = item.Groups["title"].Value,
                });
            }
            result.OtherSeriesList = otherSeriesList.ToArray();

            var postUserInfoMatch = postUserInfoRegex.Match(Value);
            result.PostUser = new Response.User()
            {
                ID = postUserInfoMatch.Groups["id"].Value,
                Name = postUserInfoMatch.Groups["name"].Value
            };

            return result;
        }
    }
}