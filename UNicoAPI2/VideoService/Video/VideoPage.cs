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
        string token = "";
        string watch_auth_key = "";
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
        /// このページの動画情報を取得する
        /// </summary>
        /// <returns></returns>
        public VideoInfo GetVideoInfo()
        {
            return target;
        }

        /// <summary>
        /// 動画の詳細情報を取得する
        /// </summary>
        public Session<Response<VideoInfo>> DownloadVideoInfo(DownloadVideoInfoUseAPI DownloadVideoInfoUseAPI)
        {
            var session = new Session<Response<VideoInfo>>();

            switch (DownloadVideoInfoUseAPI)
            {
                case DownloadVideoInfoUseAPI.html:
                    #region
                    var accessorList = new List<Func<byte[], APIs.IAccesser>>();

                    if (htmlCache == "")
                    {
                        accessorList.Add((data) =>
                        {
                            var accesser = new APIs.video_page_html.Accesser();
                            accesser.Setting(
                                context.CookieContainer,
                                target.ID);

                            return accesser;
                        });
                    }

                    session.SetAccessers(
                        accessorList.ToArray(),
                        (data) =>
                        {
                            if (htmlCache == "")
                                htmlCache = new APIs.video_page_html.Parser().Parse(data);

                            var result = new Response<VideoInfo>();
                            result.Status = Status.OK;
                            result.Result = context.IDContainer.GetVideoInfo(target.ID);
                            result.Result.Description = new APIs.video_page_html.html_video_info().Parse(htmlCache);

                            return result;
                        });

                    #endregion
                    break;
                case DownloadVideoInfoUseAPI.getthumbinfo:
                    #region
                    session.SetAccessers(new Func<byte[], APIs.IAccesser>[]
                    {
                        (data) =>
                        {
                            var accesser = new APIs.getthumbinfo.Accesser();
                            accesser.Setting(
                                context.CookieContainer,
                                target.ID);

                            return accesser;
                        }
                    },
                    (data) =>
                        Converter.VideoInfoResponse(context, new APIs.getthumbinfo.Parser().Parse(data)));
                    #endregion
                    break;
                default: throw new Exception();
            }

            return session;
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

                    var accesser = new APIs.video_page_html.Accesser();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID);

                    return accesser;
                },
                (data) =>
                {
                    htmlCache = new APIs.video_page_html.Parser().Parse(data);

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
                    Converter.Response(new APIs.upload_comment.Parser().Parse(data)));

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

                    var accesser = new APIs.download_comment.Accesser();
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
                    var Serial = new APIs.download_comment.Parser().Parse(data);
                    ticket = Serial.thread[0].ticket;
                    block_no = ((Serial.thread[0].last_res + 1) / 100).ToString();

                    return Converter.CommentResponse(Serial);
                });

            return session;
        }

        /// <summary>
        /// タグを取得する
        /// </summary>
        public Session<Response<Tag[]>> DownloadTags()
        {
            var session = new Session<Response<Tag[]>>();

            session.SetAccessers(new Func<byte[], APIs.IAccesser>[]
            {
                new Func<byte[], APIs.IAccesser>((byte[] data) =>
                {
                    var accesser = new APIs.tag_edit.DownloadAccesser();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID,
                        "json",
                        "tags");

                    return accesser;
                })
            }, (data) => Converter.TagsResponse(new APIs.tag_edit.Parser().Parse(data)));

            return session;
        }

        /// <summary>
        /// タグを追加する
        /// </summary>
        /// <param name="AddTag">追加するタグ</param>
        public Session<Response<Tag[]>> AddTag(Tag AddTag)
        {
            return EditTag(AddTag, "add");
        }

        /// <summary>
        /// タグを削除する
        /// </summary>
        /// <param name="RemoveTag">削除するタグ</param>
        public Session<Response<Tag[]>> RemoveTag(Tag RemoveTag)
        {
            return EditTag(RemoveTag, "remove");
        }

        private Session<Response<Tag[]>> EditTag(Tag EditTag, string cmd)
        {
            var session = new Session<Response<Tag[]>>();
            var accessorList = new List<Func<byte[], APIs.IAccesser>>();

            if (htmlCache == "")
            {
                accessorList.Add(new Func<byte[], APIs.IAccesser>((byte[] data) =>
                {
                    var accesser = new APIs.video_page_html.Accesser();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID);

                    return accesser;
                }));
            }

            accessorList.Add(new Func<byte[], APIs.IAccesser>((byte[] data) =>
            {
                if (htmlCache == "")
                    htmlCache = new APIs.video_page_html.Parser().Parse(data);

                if (token == "")
                    token = new APIs.video_page_html.csrf_token().Parse(htmlCache);

                if (watch_auth_key == "")
                    watch_auth_key = new APIs.video_page_html.watch_auth_key().Parse(htmlCache);

                var accesser = new APIs.tag_edit.UploadAccesser();
                accesser.Setting(
                    context.CookieContainer,
                    target.ID,
                    "json",
                    cmd,
                    EditTag.Name,
                    token,
                    watch_auth_key,
                    (EditTag.IsLock == true) ? "1" : "0");

                return accesser;
            }));

            session.SetAccessers(
                accessorList.ToArray(),
                (data) =>
                    Converter.TagsResponse(new APIs.tag_edit.Parser().Parse(data)));

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
