using System;
using System.Collections.Generic;
using UNicoAPI2.VideoService.Video;

namespace UNicoAPI2.VideoService.Converter.VideoServicePage
{
    public class Search
    {
        /*----------------------------------------*/

        public static Response<VideoInfo[]> From(Context Context, APIs.search.Response.contract Response)
        {
            var result = Converter.Response.From<VideoInfo[]>(Response.status, Response.error);
            result.Result = ToVideoInfos(Context, Response.list);

            return result;
        }

        public static VideoInfo[] ToVideoInfos(Context Context, APIs.search.Response.list[] Response)
        {
            if (Response == null) return null;

            var result = new VideoInfo[Response.Length];

            for (int i = 0; i < result.Length; i++)
            {
                var info = Context.IDContainer.GetVideoInfo(Response[i].id);

                info.ComentCounter = Response[i].num_res;
                info.Length = UNicoAPI2.Converter.TimeSpan(Response[i].length);
                info.MylistCounter = Response[i].mylist_counter;
                info.PostTime = DateTime.Parse(Response[i].first_retrieve);
                info.ShortDescription = Response[i].description_short;
                info.Title = Response[i].title;
                info.ViewCounter = Response[i].view_counter;
                info.Thumbnail = new Picture(Response[i].thumbnail_url, Context.CookieContainer);
                result[i] = info;
            }

            return result;
        }

        /*----------------------------------------*/

        public static Response<VideoInfo[]> From(Context Context, Dictionary<string, string>[] Response)
        {
            var result = Converter.Response.From<VideoInfo[]>(200, null);

            var videoList = new List<VideoInfo>();
            foreach (var item in Response)
            {
                videoList.Add(new VideoInfo()
                {
                    ID = item["id"],
                    Title = item["title"],
                    ShortDescription = item["short_desc"],
                    Length = UNicoAPI2.Converter.TimeSpan(item["length"]),
                    Thumbnail = new Picture(item["thumbnail"], Context.CookieContainer),
                    ViewCounter = int.Parse(item["view"].Replace(",", "")),
                    ComentCounter = int.Parse(item["comment"].Replace(",", "")),
                    LikeCounter = int.Parse(item["like"].Replace(",", "")),
                    MylistCounter = int.Parse(item["mylist"].Replace(",", "")),
                });
            }

            result.Result = videoList.ToArray();
            return result;
        }
    }
}
