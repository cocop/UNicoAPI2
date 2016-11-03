using System;
using System.Collections.Generic;
using UNicoAPI2.VideoService.Mylist;
using UNicoAPI2.VideoService.Video;

namespace UNicoAPI2.VideoService
{
    public static class Converter
    {
        static DateTime unixTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /********************************************/
        public static Response<VideoInfo[]> VideoInfoResponse(Context Context, APIs.search.Serial.contract Serial)
        {
            var result = new Response<VideoInfo[]>();
            Response(result, Serial.status, Serial.error);
            result.Result = VideoInfos(Context, Serial.list);

            return result;
        }

        public static Response<VideoInfo> VideoInfoResponse(Context Context, APIs.getthumbinfo.Serial.nicovideo_thumb_response Serial)
        {
            var result = new Response<VideoInfo>();

            Response(result, Serial.status, Serial.error);

            if (Serial.thumb != null)
            {
                result.Result = Context.IDContainer.GetVideoInfo(Serial.thumb.video_id);
                result.Result.ComentCounter = Serial.thumb.comment_num;
                result.Result.Description = Serial.thumb.description;
                result.Result.EconomyVideoSize = Serial.thumb.size_low;
                result.Result.IsExternalPlay = Serial.thumb.embeddable;
                result.Result.Length = UNicoAPI2.Converter.TimeSpan(Serial.thumb.length);
                result.Result.MylistCounter = Serial.thumb.mylist_counter;
                result.Result.IsLivePlay = !Serial.thumb.no_live_play;
                result.Result.PostTime = DateTime.Parse(Serial.thumb.first_retrieve);
                result.Result.Tags = Tags(Serial.thumb.tags);
                result.Result.Title = Serial.thumb.title;
                result.Result.VideoSize = Serial.thumb.size_high;
                result.Result.VideoType = Serial.thumb.movie_type;
                result.Result.ViewCounter = Serial.thumb.view_counter;
                result.Result.Thumbnail = new Picture(Serial.thumb.thumbnail_url, Context.CookieContainer);
                result.Result.User = Context.IDContainer.GetUser(Serial.thumb.user_id);
                result.Result.User.Name = Serial.thumb.user_nickname;
                result.Result.User.Icon = new Picture(Serial.thumb.user_icon_url, Context.CookieContainer);
            }

            return result;
        }

        public static Response Response(APIs.upload_comment.Serial.packet Serial)
        {
            var result = new Response();

            if (Serial.chat_result.status == "0")
                result.Status = Status.OK;
            else
            {
                switch (Serial.chat_result.status)
                {
                    case "1": result.ErrorMessage = "同じコメントを投稿しようとしました"; break;
                    case "3": result.ErrorMessage = "投稿するためのキーが足りませんでした"; break;
                    default: break;
                }
                result.Status = Status.UnknownError;
            }

            return result;
        }

        public static Response<Comment[]> CommentResponse(APIs.download_comment.Serial.packet Serial)
        {
            var result = new Response<Comment[]>();
            Response(result, "ok", null);
            result.Result = Comment(Serial.chat);

            return result;
        }

        public static Response<Tag[]> TagsResponse(APIs.tag_edit.Serial.contract Serial)
        {
            var result = new Response<Tag[]>();
            Response(result, Serial.status, new APIs.Serial.error() { description = Serial.error_msg });
            result.Result = Tags(Serial.tags);

            return result;
        }

        public static Response<Mylist.Mylist> MylistResponse(Context Context, APIs.mylistvideo.Serial.contract Serial, string MylistID)
        {
            var result = new Response<Mylist.Mylist>();

            Response(result, Serial.status, Serial.error);
            result.Result = Context.IDContainer.GetMylist(MylistID);
            result.Result.Description = Serial.description;
            result.Result.Title = Serial.name;
            result.Result.IsBookmark = Serial.is_watching_this_mylist;
            result.Result.MylistItem = MylistItem(Context, Serial.list);

            if (Serial.user_id != null)
            {
                result.Result.User = Context.IDContainer.GetUser(Serial.user_id);
                result.Result.User.Name = Serial.user_nickname;
            }

            return result;
        }

        public static Response<User.User> UserResponse(Context Context, Dictionary<string, string> Serial)
        {
            var result = new Response<User.User>();

            Response(result, "ok", null);
            result.Result = Context.IDContainer.GetUser(Serial["id"]);
            result.Result.Icon = new Picture(Serial["icon"], Context.CookieContainer);
            result.Result.Name = Serial["name"];
            result.Result.Sex = Serial["sex"];
            result.Result.Birthday = Serial["birthday"];
            result.Result.Area = Serial["area"];
            result.Result.Description = Serial["description"];
            try
            {
                result.Result.BookmarkCount = int.Parse(Serial["bookmark"]);
                result.Result.Experience = int.Parse(Serial["exp"]);
            } catch (Exception) { }

            return result;
        }

        public static Response<Mylist.Mylist[]> PublicMylistListResponse(Context context, Dictionary<string, string>[] Serial)
        {
            var result = new Response<Mylist.Mylist[]>();

            Response(result, "ok", null);
            result.Result = new Mylist.Mylist[Serial.Length];

            for (int i = 0; i < result.Result.Length; i++)
            {
                result.Result[i] = context.IDContainer.GetMylist(Serial[i]["id"]);
                result.Result[i].Title = Serial[i]["name"];
            }

            return result;
        }

        /********************************************/
        public static Tag[] Tags(APIs.tag_edit.Serial._tag[] Serial)
        {
            if (Serial == null)
                return null;

            var result = new Tag[Serial.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Tag()
                {
                    IsNicopedia = Serial[i].dic,
                    IsCategory = Serial[i].can_cat,
                    IsLock = Serial[i].owner_lock != 0,
                    Name = Serial[i].tag,
                };
            }

            return result;
        }

        public static Tag[] Tags(APIs.getthumbinfo.Serial.tags Serial)
        {
            var result = new Tag[Serial.tag.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Tag()
                {
                    IsCategory = Serial.tag[i].category != 0,
                    IsLock = Serial.tag[i]._lock != 0,
                    Name = Serial.tag[i]._tag,
                };
            }

            return result;
        }

        public static VideoInfo[] VideoInfos(Context Context, APIs.search.Serial.list[] Serial)
        {
            if (Serial == null) return null;

            var result = new VideoInfo[Serial.Length];

            for (int i = 0; i < result.Length; i++)
            {
                var info = Context.IDContainer.GetVideoInfo(Serial[i].id);

                info.ComentCounter = Serial[i].num_res;
                info.Length = UNicoAPI2.Converter.TimeSpan(Serial[i].length);
                info.MylistCounter = Serial[i].mylist_counter;
                info.PostTime = DateTime.Parse(Serial[i].first_retrieve);
                info.ShortDescription = Serial[i].description_short;
                info.Title = Serial[i].title;
                info.ViewCounter = Serial[i].view_counter;
                info.Thumbnail = new Picture(Serial[i].thumbnail_url, Context.CookieContainer);
                result[i] = info;
            }

            return result;
        }

        public static Comment[] Comment(APIs.download_comment.Serial.chat[] Serial)
        {
            var result = new Comment[(Serial != null) ? Serial.Length : 0];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Comment()
                {
                    IsAnonymity = Serial[i].anonymity,
                    Body = Serial[i].body,
                    Command = Serial[i].mail,
                    Leaf = Serial[i].leaf,
                    No = Serial[i].no,
                    PlayTime = TimeSpan.FromMilliseconds(double.Parse(Serial[i].vpos + '0')),
                    IsPremium = Serial[i].premium,
                    UserID = Serial[i].user_id,
                    WriteTime = unixTime.AddSeconds(double.Parse(Serial[i].date)).ToLocalTime(),
                    IsYourPost = Serial[i].yourpost,
                };

                try
                {
                    result[i].Scores = int.Parse(Serial[i].scores ?? "0");
                }
                catch (Exception) { }
            }

            return result;
        }

        public static MylistItem[] MylistItem(Context Context, APIs.mylistvideo.Serial.list[] Serial)
        {
            if (Serial == null)
                return null;

            var result = new MylistItem[Serial.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new MylistItem();
                result[i].Description = Serial[i].mylist_comment;
                result[i].RegisterTime = unixTime.AddSeconds(Serial[i].create_time).ToLocalTime();
                result[i].UpdateTime = DateTime.Parse(Serial[i].thread_update_time);
                result[i].VideoInfo = Context.IDContainer.GetVideoInfo(Serial[i].id);

                result[i].VideoInfo.ComentCounter = Serial[i].num_res;
                result[i].VideoInfo.ID = Serial[i].id;
                result[i].VideoInfo.Length = new TimeSpan(0, 0, Serial[i].length_seconds);
                result[i].VideoInfo.MylistCounter = Serial[i].mylist_counter;
                result[i].VideoInfo.PostTime = DateTime.Parse(Serial[i].first_retrieve);
                result[i].VideoInfo.ShortDescription = Serial[i].description_short;
                result[i].VideoInfo.Thumbnail = new Picture(Serial[i].thumbnail_url, Context.CookieContainer);
                result[i].VideoInfo.Title = Serial[i].title;
                result[i].VideoInfo.ViewCounter = Serial[i].view_counter;
            }

            return result;
        }

        /********************************************/
        public static void Response(Response Response, string Status, APIs.Serial.error Error)
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
