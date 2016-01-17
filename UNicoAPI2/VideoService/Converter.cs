using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNicoAPI2.VideoService;
using UNicoAPI2.VideoService.Video;

namespace UNicoAPI2.VideoService
{
    public static class Converter
    {
        static DateTime unixTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /********************************************/
        public static Response<VideoInfo[]> ToVideoInfoResponse(Context Context, APIs.search.Serial.contract Serial)
        {
            var result = new Response<VideoInfo[]>();
            ToResponse(result, Serial.status, Serial.error);
            result.Result = ToVideoInfos(Context, Serial.list);

            return result;
        }

        public static Response ToResponse(APIs.upload_comment.Serial.packet Serial)
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

        public static Response<Comment[]> ToCommentResponse(APIs.get_download_comment.Serial.packet Serial)
        {
            var result = new Response<Comment[]>();
            ToResponse(result, "ok", null);
            result.Result = ToComment(Serial.chat);

            return result;
        }

        /********************************************/
        public static VideoInfo[] ToVideoInfos(Context Context, APIs.search.Serial.list[] Serial)
        {
            if (Serial == null) return null;

            var result = new VideoInfo[Serial.Length];

            for (int i = 0; i < result.Length; i++)
            {
                var info = Context.IDContainer.GetVideoInfo(Serial[i].id);

                info.ComentCounter = Serial[i].num_res;
                info.Length = UNicoAPI2.Converter.ToTimeSpan(Serial[i].length);
                info.MylistCounter = Serial[i].mylist_counter;
                info.PostTime = DateTime.Parse(Serial[i].first_retrieve);
                info.ShortDescription = Serial[i].description_short;
                info.Title = Serial[i].title;
                info.ViewCounter = Serial[i].view_counter;
                //info.Thumbnail = NewPicture(info.Thumbnail, Serial[i].thumbnail_url);
                result[i] = info;
            }

            return result;
        }

        public static Comment[] ToComment(APIs.get_download_comment.Serial.chat[] Serial)
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
                    Scores = int.Parse(Serial[i].scores ?? "0"),
                    IsYourPost = Serial[i].yourpost,
                };
            }

            return result;
        }

        /********************************************/
        public static void ToResponse(Response Response, string Status, APIs.Serial.error Error)
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
                    }
                    Response.ErrorMessage = Error.description;
                    break;
            }
        }

    }
}
