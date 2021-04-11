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

        Cache<APIs.video_page_html.Response.Rootobject> htmlCache = new Cache<APIs.video_page_html.Response.Rootobject>();
        Cache<NameValueCollection> videoCache = new Cache<NameValueCollection>();

        bool isAvailabDmcCash = false;

        string ticket = "";
        string block_no = "";
        string postkey = "";


        /******************************************/
        /******************************************/

        internal VideoPage(VideoInfo Target, Context Context)
        {
            target = Target;
            context = Context;

            htmlCache.ChangedValue += () =>
            {
                isAvailabDmcCash = true;
            };
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
                    var accessorList = new List<Func<byte[], APIs.IAccessor>>();

                    if (!htmlCache.IsAvailab)
                    {
                        accessorList.Add((data) =>
                        {
                            var accesser = new APIs.video_page_html.Accessor();
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
                            var parser = new APIs.video_page_html.Parser();

                            if (data != null)
                                htmlCache.Value = parser.Parse(parser.Parse(data));

                            return Converter.VideoInfoResponse(
                                context,
                                htmlCache.Value);
                        });

                    #endregion
                    break;
                case DownloadVideoInfoUseAPI.getthumbinfo:
                    #region
                    session.SetAccessers(new Func<byte[], APIs.IAccessor>[]
                    {
                        (data) =>
                        {
                            var accesser = new APIs.getthumbinfo.Accessor();
                            accesser.Setting(
                                context.CookieContainer,
                                target.ID);

                            return accesser;
                        }
                    },
                    (data) =>
                    {
                        return Converter.VideoInfoResponse(
                            context,
                            new APIs.getthumbinfo.Parser().Parse(data));
                    });
                    #endregion
                    break;
                default: throw new Exception();
            }

            return session;
        }

        /// <summary>
        /// 動画をダウンロードする
        /// </summary>
        public Session<WebResponse> DownloadVideo(DownloadVideoUseAPI DownloadVideoUseAPI = DownloadVideoUseAPI.getflv)
        {
            return DownloadVideo(0, -1, DownloadVideoUseAPI);
        }

        public Session<WebResponse> DownloadVideo(long Position, long Length, DownloadVideoUseAPI DownloadVideoUseAPI)
        {
            switch (DownloadVideoUseAPI)
            {
                case DownloadVideoUseAPI.getflv: return DownloadVideoUseGetflv(Position, Length);
                default: return null;
            }
        }

        /// <summary>
        /// getflvAPIを使用して動画を取得する
        /// </summary>
        private Session<WebResponse> DownloadVideoUseGetflv(long Position, long Length)
        {
            var session = new Session<WebResponse>();
            var accessorList = new List<Func<byte[], APIs.IAccessor>>();
            var isVideoCacheProgress = false;
            var isHtmlCacheProgress = false;

            if (!videoCache.IsAvailab)
            {
                isVideoCacheProgress = true;
                accessorList.Add((data) =>
                {
                    var accesser = new APIs.getflv.Accessor();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID);

                    return accesser;
                });
            }

            if (!htmlCache.IsAvailab)
            {
                isHtmlCacheProgress = true;
                accessorList.Add(
                    (data) =>
                    {
                        if (data != null)
                            videoCache.Value = new APIs.getflv.Parser().Parse(data);

                        var accesser = new APIs.video_page_html.Accessor();
                        accesser.Setting(
                            context.CookieContainer,
                            target.ID);

                        return accesser;
                    });
            }

            accessorList.Add(
                (data) =>
                {
                    if (data != null)
                        if (isHtmlCacheProgress)
                        {
                            var parser = new APIs.video_page_html.Parser();
                            htmlCache.Value = parser.Parse(parser.Parse(data));
                        }
                        else if (isVideoCacheProgress)
                            videoCache.Value = new APIs.getflv.Parser().Parse(data);

                    var accesser = new APIs.FileDownloadAccessor();
                    accesser.Setting(
                        context.CookieContainer,
                        videoCache.Value["url"],
                        Position,
                        Length);

                    return accesser;
                });

            session.SetAccessers(accessorList.ToArray(), null);
            return session;
        }

        /// <summary>
        /// html5APIを使用して動画を取得する
        /// </summary>
        /// <returns>接続維持セッションを持ったVideoSource</returns>
        public Session<DmcVideoSource> GetDmcVideoSource()
        {
            var session = new Session<DmcVideoSource>();
            var accessorList = new List<Func<byte[], APIs.IAccessor>>();

            if (!htmlCache.IsAvailab || !isAvailabDmcCash || DateTime.Now > (htmlCache.GotTime?.AddSeconds(30) ?? DateTime.MinValue))
            {
                accessorList.Add((data) =>
                {
                    var accesser = new APIs.video_page_html.Accessor();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID);

                    return accesser;
                });
            }
            isAvailabDmcCash = false;

            accessorList.Add((data) =>
            {
                if (data != null)
                {
                    var parser = new APIs.video_page_html.Parser();
                    htmlCache.Value = parser.Parse(parser.Parse(data));
                }

                var accesser = new APIs.media_session.Accessor();
                accesser.Setting(
                    context.CookieContainer,
                    htmlCache.Value.media);

                return accesser;
            });

            accessorList.Add((data) =>
            {
                var accesser = new APIs.media_session.Accessor();
                accesser.Setting(
                    context.CookieContainer,
                    htmlCache.Value.media);

                return accesser;
            });

            session.SetAccessers(accessorList.ToArray(), (data) =>
            {
                var parser = new APIs.heartbeats.Parser();
                return new DmcVideoSource(context.CookieContainer, new Uri(htmlCache.Value.media.delivery.movie.session.urls[0].url), parser.Parse(data));
            });
            return session;
        }


        /// <summary>
        /// コメントをアップロードする
        /// </summary>
        /// <param name="Comment">投稿するコメント</param>
        public Session<Response> UploadComment(Comment Comment)
        {
            var session = new Session<Response>();
            var accessorList = new List<Func<byte[], APIs.IAccessor>>();

            if (!videoCache.IsAvailab)
                accessorList.Add((data) =>
                {
                    var accesser = new APIs.getflv.Accessor();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID);

                    return accesser;
                });

            if (postkey == "")
                accessorList.Add((data) =>
                {
                    if (data != null)
                        videoCache.Value = new APIs.getflv.Parser().Parse(data);

                    var accesser = new APIs.getpostkey.Accessor();
                    accesser.Setting(
                        context.CookieContainer,
                        block_no,
                        videoCache.Value["thread_id"]);

                    return accesser;
                });

            accessorList.Add((data) =>
            {
                if (postkey == "")
                    postkey = new APIs.getpostkey.Parser().Parse(data);

                var accesser = new APIs.upload_comment.Accessor();
                accesser.Setting(
                    context.CookieContainer,
                    videoCache.Value["ms"],
                    videoCache.Value["thread_id"],
                    ((int)(Comment.PlayTime.TotalMilliseconds / 10)).ToString(),
                    Comment.Command,
                    ticket,
                    videoCache.Value["user_id"],
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
            var accessorList = new List<Func<byte[], APIs.IAccessor>>();

            if (!videoCache.IsAvailab)
            {
                accessorList.Add((data) =>
                {
                    var accesser = new APIs.getflv.Accessor();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID);

                    return accesser;
                });
            }

            accessorList.AddRange(new Func<byte[], APIs.IAccessor>[]
            {
                (data) =>
                {
                    if (data != null)
                        videoCache.Value = new APIs.getflv.Parser().Parse(data);

                    var accesser = new APIs.download_comment.Accessor();
                    accesser.Setting(
                        context.CookieContainer,
                        videoCache.Value["ms"],
                        videoCache.Value["thread_id"]);

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
        /// キャッシュを削除する
        /// </summary>
        public void ClearCache()
        {
            videoCache.Clear();
            htmlCache.Clear();
        }
    }
}
