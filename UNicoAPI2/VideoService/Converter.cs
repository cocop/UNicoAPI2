using System;
using System.Collections.Generic;
using System.Linq;
using UNicoAPI2.VideoService.Mylist;
using UNicoAPI2.VideoService.Video;

namespace UNicoAPI2.VideoService
{
    public static class Converter
    {
        static DateTime unixTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /********************************************/
        public static Response<VideoInfo[]> VideoInfoResponse(Context Context, APIs.search.Response.contract Response)
        {
            var result = new Response<VideoInfo[]>();
            Converter.Response(result, Response.status, Response.error);
            result.Result = VideoInfos(Context, Response.list);

            return result;
        }

        public static Response<VideoInfo[]> VideoInfoResponse(Context Context, Dictionary<string, string>[] Response)
        {
            var result = new Response<VideoInfo[]>();
            Converter.Response(result, "ok", null);

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

        public static Response<VideoInfo> VideoInfoResponse(Context Context, APIs.getthumbinfo.Response.nicovideo_thumb_response Response)
        {
            var result = new Response<VideoInfo>();

            Converter.Response(result, Response.status, Response.error);

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
                result.Result.Tags = Tags(Response.thumb.tags);
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

        public static Response<VideoInfo> VideoInfoResponse(Context Context, APIs.video_page_html.Response.Rootobject Response)
        {
            var result = new Response<VideoInfo>();
            result.Status = Status.OK;
            result.Result = Context.IDContainer.GetVideoInfo(Response.video.id);

            result.Result.ComentCounter = Response.video.count.comment;
            result.Result.Description = Response.video.description;
            result.Result.IsExternalPlay = Response.video.isEmbedPlayerAllowed;
            result.Result.Length = TimeSpan.FromSeconds(Response.video.duration);
            result.Result.MylistCounter = Response.video.count.mylist;
            result.Result.PostTime = DateTime.Parse(Response.video.registeredAt);
            result.Result.Tags = Tags(Response.tag);
            result.Result.Title = Response.video.title;
            result.Result.ViewCounter = Response.video.count.view;
            result.Result.LikeCounter = Response.video.count.like;
            result.Result.Thumbnail = new Picture(Response.video.thumbnail.url, Context.CookieContainer);
            result.Result.User = User(Context, Response.owner);
            if (Response?.series != null)
            {
                result.Result.Series = Context.IDContainer.GetSeries(Response.series.id.ToString());
                result.Result.Series.Title = Response.series.title;
                result.Result.First = VideoInfo(Context, Response.series?.video?.first);
                result.Result.Next = VideoInfo(Context, Response.series?.video?.next);
                result.Result.Prev = VideoInfo(Context, Response.series?.video?.prev);
            }
            return result;
        }

        public static Response Response(APIs.upload_comment.Response.packet Response)
        {
            var result = new Response();

            if (Response.chat_result.status == "0")
                result.Status = Status.OK;
            else
            {
                switch (Response.chat_result.status)
                {
                    case "1": result.ErrorMessage = "同じコメントを投稿しようとしました"; break;
                    case "3": result.ErrorMessage = "投稿するためのキーが足りませんでした"; break;
                    default: break;
                }
                result.Status = Status.UnknownError;
            }

            return result;
        }

        public static Response<Comment[]> CommentResponse(APIs.download_comment.Response.packet Response)
        {
            var result = new Response<Comment[]>();
            Converter.Response(result, "ok", null);
            result.Result = Comment(Response.chat);

            return result;
        }

        public static Response<Tag[]> TagsResponse(APIs.tag_edit.Response.contract Response)
        {
            var result = new Response<Tag[]>();
            Converter.Response(result, Response.status, new APIs.Response.error() { description = Response.error_msg });
            result.Result = Tags(Response.tags);

            return result;
        }

        public static Response<Mylist.Mylist> MylistResponse(Context Context, APIs.mylitv2.Response.Rootobject Response, string MylistID)
        {
            var result = new Response<Mylist.Mylist>();

            if (Response.meta.status < 200 && 300 <= Response.meta.status)
            {
                result.Status = Status.UnknownError;
                result.ErrorMessage = Response.meta.errorCode;
                return result;
            }

            result.Status = Status.OK;
            result.Result = Context.IDContainer.GetMylist(MylistID);
            result.Result.Description = Response.data.mylist.description;
            result.Result.Title = Response.data.mylist.name;
            result.Result.IsBookmark = Response.data.mylist.isFollowing;
            result.Result.MylistItem = MylistItem(Context, Response.data.mylist.items);

            if (Response.data.mylist.owner != null)
            {
                result.Result.User = Context.IDContainer.GetUser(Response.data.mylist.owner.id);
                result.Result.User.Name = Response.data.mylist.owner.name;
                result.Result.User.Icon = new Picture(Response.data.mylist.owner.iconUrl, Context.CookieContainer);
            }

            return result;
        }

        public static Response<Series.Series> SeriesResponse(Context Context, APIs.series_page_html.Response.Result Response, string SeriesID)
        {
            var result = new Response<Series.Series>();

            Converter.Response(result, "ok", null);
            result.Result = Context.IDContainer.GetSeries(SeriesID);
            result.Result.ID = SeriesID;
            result.Result.Title = Response.Title;
            result.Result.User = Context.IDContainer.GetUser(Response.PostUser.ID);
            result.Result.User.Name = Response.PostUser?.Name;
            result.Result.OtherSeriesList = Response.OtherSeriesList?.Select((otherSeries) =>
            {
                var series = Context.IDContainer.GetSeries(otherSeries.ID);
                series.Title = otherSeries.Title;
                return series;
            }).ToArray();
            result.Result.VideoList = Response.VideoList?.Select((video) =>
            {
                var videoInfo = Context.IDContainer.GetVideoInfo(video.ID);
                videoInfo.Title = video.Title;
                videoInfo.Thumbnail = new Picture(video.ThumbnailUrl, Context.CookieContainer);
                videoInfo.User = result.Result.User;
                return videoInfo;
            }).ToArray();

            return result;
        }

        public static Response<User.User> UserResponse(Context Context, Dictionary<string, string> Response)
        {
            var result = new Response<User.User>();

            Converter.Response(result, "ok", null);
            result.Result = Context.IDContainer.GetUser(Response["id"]);
            result.Result.Icon = new Picture(Response["icon"], Context.CookieContainer);
            result.Result.Name = Response["name"];

            result.Result.Description = Response["description"];
            try
            {
                result.Result.BookmarkCount = int.Parse(Response["bookmark"]);
                result.Result.Experience = int.Parse(Response["exp"]);
            }
            catch (Exception) { }

            return result;
        }

        public static Response<Mylist.Mylist[]> PublicMylistListResponse(Context context, Dictionary<string, string>[] Response)
        {
            var result = new Response<Mylist.Mylist[]>();

            Converter.Response(result, "ok", null);
            result.Result = new Mylist.Mylist[Response.Length];

            for (int i = 0; i < result.Result.Length; i++)
            {
                result.Result[i] = context.IDContainer.GetMylist(Response[i]["id"]);
                result.Result[i].Title = Response[i]["name"];
            }

            return result;
        }

        /********************************************/
        public static User.User User(Context Context, APIs.video_page_html.Response.Rootobject.Owner Response)
        {
            var user = Context.IDContainer.GetUser(Response?.id);
            user.Name = Response?.nickname;
            user.Icon = new Picture(Response?.iconUrl, Context.CookieContainer);
            return user;
        }

        public static VideoInfo VideoInfo(Context Context, APIs.video_page_html.Response.Rootobject.Series.Video.Item Response)
        {
            if (Response == null)
            {
                return null;
            }

            return new VideoInfo(Response.id)
            {
                Title = Response.title,
                ID = Response.id,
                User = User(Context, Response.owner),
                Thumbnail = new Picture(Response.thumbnail.url, Context.CookieContainer),
            };
        }

        public static User.User User(Context Context, APIs.video_page_html.Response.Rootobject.Series.Video.Item.Owner Response)
        {
            var user = Context.IDContainer.GetUser(Response?.id);
            user.Name = Response?.name;
            user.Icon = new Picture(Response?.iconUrl, Context.CookieContainer);
            return user;
        }

        public static Tag[] Tags(APIs.tag_edit.Response._tag[] Response)
        {
            if (Response == null)
                return null;

            var result = new Tag[Response.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Tag()
                {
                    IsNicopedia = Response[i].dic,
                    IsCategory = Response[i].can_cat,
                    IsLock = Response[i].owner_lock != 0,
                    Name = Response[i].tag,
                };
            }

            return result;
        }

        public static Tag[] Tags(APIs.getthumbinfo.Response.tags Response)
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

        private static Tag[] Tags(APIs.video_page_html.Response.Rootobject.Tag Response)
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

        public static VideoInfo[] VideoInfos(Context Context, APIs.search.Response.list[] Response)
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

        public static Comment[] Comment(APIs.download_comment.Response.chat[] Response)
        {
            var result = new Comment[(Response != null) ? Response.Length : 0];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Comment()
                {
                    IsAnonymity = Response[i].anonymity,
                    Body = Response[i].body,
                    Command = Response[i].mail,
                    Leaf = Response[i].leaf,
                    No = Response[i].no,
                    PlayTime = TimeSpan.FromMilliseconds(double.Parse(Response[i].vpos + '0')),
                    IsPremium = Response[i].premium,
                    UserID = Response[i].user_id,
                    WriteTime = unixTime.AddSeconds(double.Parse(Response[i].date)).ToLocalTime(),
                    IsYourPost = Response[i].yourpost,
                };

                try
                {
                    result[i].Scores = int.Parse(Response[i].scores ?? "0");
                }
                catch (Exception) { }
            }

            return result;
        }

        public static MylistItem[] MylistItem(Context Context, APIs.mylitv2.Response.Item[] Response)
        {
            if (Response == null)
                return null;

            var result = new MylistItem[Response.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new MylistItem();
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

        /********************************************/
        public static void Response(Response Response, string Status, APIs.Response.error Error)
        {
            switch (Status)
            {
                case "ok":
                    Response.Status = VideoService.Status.OK;
                    break;
                case "fail":
                    if (Error == null)
                    {
                        Response.Status = VideoService.Status.UnknownError;
                        break;
                    }

                    switch (Error.code)
                    {
                        case "DELETED":
                            Response.Status = VideoService.Status.Deleted;
                            break;
                        default:
                            Response.Status = VideoService.Status.UnknownError;
                            break;
                    }
                    Response.ErrorMessage = Error.description;
                    break;
            }
        }

    }
}
