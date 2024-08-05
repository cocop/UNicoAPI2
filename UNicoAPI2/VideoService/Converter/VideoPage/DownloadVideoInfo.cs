using System;
using UNicoAPI2.VideoService.Video;

namespace UNicoAPI2.VideoService.Converter.VideoPage
{
    public class DownloadVideoInfo
    {
        /*----------------------------------------*/
        public static Response<VideoInfo> From(Context Context, APIs.ms.getthumbinfo.Response.nicovideo_thumb_response Response)
        {
            var result = Converter.Response.From<VideoInfo>(Response.status, Response.error);

            if (Response.thumb != null)
            {
                result.Result = Context.IDContainer.GetVideoInfo(Response.thumb.video_id);
                result.Result.ComentCounter = Response.thumb.comment_num;
                result.Result.Description = Response.thumb.description;
                result.Result.EconomyVideoSize = Response.thumb.size_low;
                result.Result.IsExternalPlay = Response.thumb.embeddable;
                result.Result.Length = UNicoAPI2.Converter.TimeSpan(Response.thumb.length);
                result.Result.MylistCounter = Response.thumb.mylist_counter;
                result.Result.IsLivePlay = !Response.thumb.no_live_play;
                result.Result.PostTime = DateTime.Parse(Response.thumb.first_retrieve);
                result.Result.Tags = ToTags(Response.thumb.tags);
                result.Result.Title = Response.thumb.title;
                result.Result.VideoSize = Response.thumb.size_high;
                result.Result.VideoType = Response.thumb.movie_type;
                result.Result.ViewCounter = Response.thumb.view_counter;
                result.Result.Thumbnail = new Picture(Response.thumb.thumbnail_url, Context.CookieContainer);
                result.Result.User = Context.IDContainer.GetUser(Response.thumb.user_id);
                result.Result.User.Name = Response.thumb.user_nickname;
                result.Result.User.Icon = new Picture(Response.thumb.user_icon_url, Context.CookieContainer);
            }

            return result;
        }

        public static VideoInfo VideoInfo(Context Context, APIs.html.video_page.Response.Rootobject.Data.Response.Series.Video.Item Response)
        {
            if (Response == null)
            {
                return null;
            }

            return new VideoInfo(Response.id)
            {
                Title = Response.title,
                ID = Response.id,
                User = ToUser(Context, Response.owner),
                Thumbnail = new Picture(Response.thumbnail.url, Context.CookieContainer),
            };
        }

        public static User.User ToUser(Context Context, APIs.html.video_page.Response.Rootobject.Data.Response.Series.Video.Item.Owner Response)
        {
            var user = Context.IDContainer.GetUser(Response?.id);
            user.Name = Response?.name;
            user.Icon = new Picture(Response?.iconUrl, Context.CookieContainer);
            return user;
        }

        public static Tag[] ToTags(APIs.ms.getthumbinfo.Response.tags Response)
        {
            var result = new Tag[Response.tag.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Tag()
                {
                    IsCategory = Response.tag[i].category != 0,
                    IsLock = Response.tag[i]._lock != 0,
                    Name = Response.tag[i]._tag,
                };
            }

            return result;
        }

        /*----------------------------------------*/

        public static Response<VideoInfo> From(Context Context, APIs.html.video_page.Response.Rootobject Response)
        {
            var result = new Response<VideoInfo>();
            result.Status = Status.OK;
            result.Result = Context.IDContainer.GetVideoInfo(Response.data.response.video.id);

            result.Result.ComentCounter = Response.data.response.video.count.comment;
            result.Result.Description = Response.data.response.video.description;
            result.Result.IsExternalPlay = Response.data.response.video.isEmbedPlayerAllowed;
            result.Result.Length = TimeSpan.FromSeconds(Response.data.response.video.duration);
            result.Result.MylistCounter = Response.data.response.video.count.mylist;
            result.Result.PostTime = DateTime.Parse(Response.data.response.video.registeredAt);
            result.Result.Tags = ToTags(Response.data.response.tag);
            result.Result.Title = Response.data.response.video.title;
            result.Result.ViewCounter = Response.data.response.video.count.view;
            result.Result.LikeCounter = Response.data.response.video.count.like;
            result.Result.Thumbnail = new Picture(Response.data.response.video.thumbnail.url, Context.CookieContainer);
            result.Result.User = ToUser(Context, Response.data.response.owner);
            if (Response?.data.response.series != null)
            {
                result.Result.Series = Context.IDContainer.GetSeries(Response.data.response.series.id.ToString());
                result.Result.Series.Title = Response.data.response.series.title;
                result.Result.First = VideoInfo(Context, Response.data.response.series?.video?.first);
                result.Result.Next = VideoInfo(Context, Response.data.response.series?.video?.next);
                result.Result.Prev = VideoInfo(Context, Response.data.response.series?.video?.prev);
            }
            return result;
        }

        public static User.User ToUser(Context Context, APIs.html.video_page.Response.Rootobject.Data.Response.Owner Response)
        {
            var user = Context.IDContainer.GetUser(Response?.id.ToString());
            user.Name = Response?.nickname;
            user.Icon = new Picture(Response?.iconUrl, Context.CookieContainer);
            return user;
        }

        private static Tag[] ToTags(APIs.html.video_page.Response.Rootobject.Data.Response.Tag Response)
        {
            var result = new Tag[Response.items.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Tag()
                {
                    IsCategory = Response.items[i].isCategory,
                    IsLock = Response.items[i].isLocked,
                    Name = Response.items[i].name,
                };
            }

            return result;
        }
    }
}
