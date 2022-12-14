using System;

namespace UNicoAPI2.VideoService.Converter.MylistPage
{
    public class DownloadMylist
    {
        public static Response<Mylist.Mylist> From(Context Context, APIs.mylitv2.Response.Rootobject Response, string MylistID)
        {
            var result = Converter.Response.From<Mylist.Mylist>(Response.meta.status, Response.meta.errorCode);

            if (result.Status != Status.OK)
            {
                return result;
            }

            result.Result = Context.IDContainer.GetMylist(MylistID);
            result.Result.Description = Response.data.mylist.description;
            result.Result.Title = Response.data.mylist.name;
            result.Result.IsBookmark = Response.data.mylist.isFollowing;
            result.Result.MylistItem = ToMylistItem(Context, Response.data.mylist.items);

            if (Response.data.mylist.owner != null)
            {
                result.Result.User = Context.IDContainer.GetUser(Response.data.mylist.owner.id);
                result.Result.User.Name = Response.data.mylist.owner.name;
                result.Result.User.Icon = new Picture(Response.data.mylist.owner.iconUrl, Context.CookieContainer);
            }

            return result;
        }


        public static Mylist.MylistItem[] ToMylistItem(Context Context, APIs.mylitv2.Response.Item[] Response)
        {
            if (Response == null)
                return null;

            var result = new Mylist.MylistItem[Response.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Mylist.MylistItem();
                result[i].Description = Response[i].description;
                result[i].RegisterTime = Response[i].addedAt;
                result[i].VideoInfo = Context.IDContainer.GetVideoInfo(Response[i].watchId);
                result[i].VideoInfo.Length = TimeSpan.FromSeconds(Response[i].video.duration);
                result[i].VideoInfo.PostTime = Response[i].video.registeredAt;
                result[i].VideoInfo.ShortDescription = Response[i].video.shortDescription;
                result[i].VideoInfo.Thumbnail = new Picture(Response[i].video.thumbnail.url, Context.CookieContainer);
                result[i].VideoInfo.Title = Response[i].video.title;
                result[i].VideoInfo.ComentCounter = Response[i].video.count.comment;
                result[i].VideoInfo.MylistCounter = Response[i].video.count.mylist;
                result[i].VideoInfo.ViewCounter = Response[i].video.count.view;
            }

            return result;
        }
    }
}
