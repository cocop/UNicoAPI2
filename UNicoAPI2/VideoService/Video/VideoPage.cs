using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using UNicoAPI2.Connect;

namespace UNicoAPI2.VideoService.Video
{

    /******************************************/
    /// <summary>
    /// 動画ページへアクセスする
    /// </summary>
    /******************************************/
    public class VideoPage
    {
        VideoInfo target;
        Context context;
        string ticket = "";
        string block_no = "";
        string postkey = "";
        string htmlCache = "";

        NameValueCollection videoCache;

        /******************************************/
        /******************************************/

        internal VideoPage(VideoInfo Target, Context Context)
        {
            target = Target;
            context = Context;
        }


        /// <summary>
        /// 動画をダウンロードする
        /// </summary>
        public Session<WebResponse> DownloadVideo()
        {

            var session = new Session<WebResponse>();
            var accessorList = new List<Func<byte[], APIs.IAccesser>>();

            if (videoCache == null)
            {
                accessorList.Add((data) =>
                {
                    var accesser = new APIs.getflv.Accesser();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID);

                    return accesser;
                });
            }
            accessorList.AddRange(new Func<byte[], APIs.IAccesser>[]
            {
                (data) =>
                {
                    if (videoCache == null)
                        videoCache = new APIs.getflv.Parser().Parse(data);

                    var accesser = new APIs.videopagehtml.Accesser();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID);

                    return accesser;
                },
                (data) =>
                {
                    htmlCache = new APIs.videopagehtml.Parser().Parse(data);

                    var accesser = new APIs.getvideo.Accesser();
                    accesser.Setting(
                        context.CookieContainer,
                        videoCache["url"]);

                    return accesser;
                },
            });

            session.SetAccessers(accessorList.ToArray(), null);
            return session;
        }

        /// <summary>
        /// コメントをアップロードする
        /// </summary>
        /// <param name="Comment">投稿するコメント</param>
        public Session<Response> UploadComment(Comment Comment)
        {
            var session = new Session<Response>();
            var accessorList = new List<Func<byte[], APIs.IAccesser>>();

            if (videoCache == null)
                accessorList.Add((data) =>
                {
                    var accesser = new APIs.getflv.Accesser();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID);

                    return accesser;
                });

            if (postkey == "")
                accessorList.Add((data) =>
                {
                    if (videoCache == null)
                        videoCache = new APIs.getflv.Parser().Parse(data);

                    var accesser = new APIs.getpostkey.Accesser();
                    accesser.Setting(
                        context.CookieContainer,
                        block_no,
                        videoCache["thread_id"]);

                    return accesser;
                });

            accessorList.Add((data) =>
            {
                if (postkey == "")
                    postkey = new APIs.getpostkey.Parser().Parse(data);

                var accesser = new APIs.upload_comment.Accesser();
                accesser.Setting(
                    context.CookieContainer,
                    videoCache["ms"],
                    videoCache["thread_id"],
                    ((int)(Comment.PlayTime.TotalMilliseconds / 10)).ToString(),
                    Comment.Command,
                    ticket,
                    videoCache["user_id"],
                    postkey,
                    Comment.Body);

                return accesser;
            });


            session.SetAccessers(
                accessorList.ToArray(),
                (data) =>
                    Converter.ToResponse(new APIs.upload_comment.Parser().Parse(data)));

            return session;
        }

        /// <summary>
        /// コメントをダウンロードする
        /// </summary>
        public Session<Response<Comment[]>> DownloadComment()
        {
            var session = new Session<Response<Comment[]>>();
            var accessorList = new List<Func<byte[], APIs.IAccesser>>();

            if (videoCache == null)
            {
                accessorList.Add((data) =>
                {
                    var accesser = new APIs.getflv.Accesser();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID);

                    return accesser;
                });
            }
            accessorList.AddRange(new Func<byte[], APIs.IAccesser>[]
            {
                (data) =>
                {
                    if (videoCache == null)
                        videoCache = new APIs.getflv.Parser().Parse(data);

                    var accesser = new APIs.get_download_comment.Accesser();
                    accesser.Setting(
                        context.CookieContainer,
                        videoCache["ms"],
                        videoCache["thread_id"]);

                    return accesser;
                },
            });

            session.SetAccessers(
                accessorList.ToArray(),
                (data) =>
                {
                    var Serial = new APIs.get_download_comment.Parser().Parse(data);
                    ticket = Serial.thread[0].ticket;
                    block_no = ((Serial.thread[0].last_res + 1) / 100).ToString();

                    return Converter.ToCommentResponse(Serial);
                });

            return session;
        }

        /// <summary>
        /// キャッシュを削除する
        /// </summary>
        public void ClearCache()
        {
            videoCache = null;
            ticket = "";
            block_no = "";
            postkey = "";
            htmlCache = "";
        }
    }
}
