using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UNicoAPI2.APIs.series_page_html.Serial;

namespace UNicoAPI2.APIs.series_page_html
{
    public class Parser : IHtmlParser<Result>
    {
        static readonly Regex titleRegex = new Regex("<div class=\"SeriesDetailContainer-bodyTitle\">(?<title>.*?)</div>");
        static readonly Regex videoInfoRegex = new Regex(@"<div class=""MediaObject-body"">
        <div class=""SeriesVideoListContainer-videoRegisteredAt"">
          (?<post_time>.*?) 投稿
      </div>
    <div class=""VideoMediaObject-title""><a href=""/watch/(?<id>.*?)"" data-href="".*?"">(?<title>.*?)</a></div><div class=""VideoMediaObject-description"">(?<description>.*?)</div>      <div class=""SeriesVideoListContainer-videoMetaCount"">
        <span class=""VideoMetaCount VideoMetaCount-view"">(?<view_count>.*?)</span>
        <span class=""VideoMetaCount VideoMetaCount-comment"">(?<comment_count>.*?)</span>
        <span class=""VideoMetaCount VideoMetaCount-mylist"">(?<mylist_count>.*?)</span>
      </div>
    </div>
  </div>");
        static readonly Regex postUserInfoRegex = new Regex("<a class=\"SeriesAdditionalContainer-ownerName\" href=\"https://www.nicovideo.jp/user/(?<id>.*?)\">(?<name>.*?)</a>");

        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }

        public Result Parse(string Value)
        {
            var result = new Result();

            result.Title = titleRegex.Match(Value).Value;

            var videoList = new List<Video>();
            foreach (Match item in videoInfoRegex.Matches(Value))
            {
                videoList.Add(new Video()
                {
                    Title = item.Groups["title"].Value,
                    ID = item.Groups["id"].Value,
                    Description = item.Groups["description"].Value,
                    ViewCount = item.Groups["view_count"].Value,
                    CommentCount = item.Groups["comment_count"].Value,
                    MylistCount = item.Groups["mylist_count"].Value,
                });
            }
            result.VideoList = videoList.ToArray();

            var postUserInfoMatch = postUserInfoRegex.Match(Value);
            result.PostUser = new User()
            {
                ID = postUserInfoMatch.Groups["id"].Value,
                Name = postUserInfoMatch.Groups["name"].Value
            };

            return result;
        }
    }
}
