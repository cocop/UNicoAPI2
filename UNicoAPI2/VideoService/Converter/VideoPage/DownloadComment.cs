using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNicoAPI2.VideoService.Converter.VideoPage
{
    public class DownloadComment
    {
        /*----------------------------------------*/

        static DateTime unixTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static Response<Video.Comment[]> From(APIs.download_comment.Response.packet Response)
        {
            var result = Converter.Response.From<Video.Comment[]>(200, null);

            result.Result = Comment(Response.chat);
            return result;
        }

        public static Video.Comment[] Comment(APIs.download_comment.Response.chat[] Response)
        {
            var result = new Video.Comment[(Response != null) ? Response.Length : 0];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Video.Comment()
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

        /*----------------------------------------*/

        public static Response<Video.Comment[]> From(APIs.nvcomment.get.Response.Rootobject Response)
        {
            var result = Converter.Response.From<Video.Comment[]>(Response.meta.status, Response.meta.message);
            result.Result = new Video.Comment[Response.data.threads.Sum((i) => i.comments.Length)];

            var resultIndex = 0;
            for (int i = 0; i < Response.data.threads.Length; i++)
            {
                for (int j = 0; j < Response.data.threads[i].comments.Length; j++)
                {
                    var cmment = Response.data.threads[i].comments[j];

                    result.Result[resultIndex] = new Video.Comment
                    {
                        PostUserType = Video.CommentTypeGen.From(Response.data.threads[i].fork),
                        Body = cmment.body,
                        Command = string.Join(" ", cmment.commands),
                        UserID = cmment.userId,
                        IsYourPost = cmment.isMyPost,
                        IsPremium = cmment.isPremium,
                        No = cmment.no,
                        WriteTime = cmment.postedAt,
                        PlayTime = TimeSpan.FromMilliseconds(cmment.vposMs)
                    };
                    resultIndex++;
                }
            }

            Array.Sort(result.Result, (x, y) => (int)(x.PlayTime.TotalMilliseconds - y.PlayTime.TotalMilliseconds));

            return result;
        }
    }
}
