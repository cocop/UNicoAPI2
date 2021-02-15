﻿using System;
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

        string ticket = "";
        string block_no = "";
        string postkey = "";


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
                case DownloadVideoUseAPI.Html5: return DownloadVideoUseHtml5(Position, Length);
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

            accessorList.Add((data) =>
            {
                var parser = new APIs.video_page_html.Parser();
                htmlCache.Value = parser.Parse(parser.Parse(data));

                var accesser = new APIs.dmc_session.Accessor();
                accesser.Setting(
                    context.CookieContainer,
                    htmlCache.Value.video.dmcInfo);

                return accesser;
            });

            accessorList.Add((data) =>
            {
                var parser = new APIs.video_page_html.Parser();
                htmlCache.Value = parser.Parse(parser.Parse(data));

                var accesser = new APIs.dmc_session.Accessor();
                accesser.Setting(
                    context.CookieContainer,
                    htmlCache.Value.video.dmcInfo);

                return accesser;
            });

            session.SetAccessers(accessorList.ToArray(), (data) =>
            {
                var parser = new APIs.heartbeats.Parser();
                return new DmcVideoSource(context.CookieContainer, parser.Parse(data));
            });
            return session;
        }

        /// <summary>
        /// Html内のurlから動画を取得する
        /// </summary>
        private Session<WebResponse> DownloadVideoUseHtml5(long Position, long Length)
        {
            var session = new Session<WebResponse>();
            var accessorList = new List<Func<byte[], APIs.IAccessor>>();
            var isHtmlCacheProgress = false;

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

            accessorList.AddRange(
                new Func<byte[], APIs.IAccessor>[]
                {
                    (data) =>
                    {
                        var parser = new APIs.video_page_html.Parser();

                        if (data != null)
                            if (isHtmlCacheProgress)
                                htmlCache.Value = parser.Parse(parser.Parse(data));

                        var accesser = new APIs.dmc_session.Accessor();
                        accesser.Setting(
                            context.CookieContainer,
                            htmlCache.Value.video.dmcInfo);

                        return accesser;
                    },
                    (data) =>
                    {
                        var parser = new APIs.video_page_html.Parser();
                        var dmcInfo = parser.Parse(data);

                        return null;
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
        /// タグを取得する
        /// </summary>
        public Session<Response<Tag[]>> DownloadTags()
        {
            var session = new Session<Response<Tag[]>>();

            session.SetAccessers(new Func<byte[], APIs.IAccessor>[]
            {
                new Func<byte[], APIs.IAccessor>((byte[] data) =>
                {
                    var accesser = new APIs.tag_edit.DownloadAccessor();
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
            var accessorList = new List<Func<byte[], APIs.IAccessor>>();

            if (!htmlCache.IsAvailab)
            {
                accessorList.Add(new Func<byte[], APIs.IAccessor>((byte[] data) =>
                {
                    var accesser = new APIs.video_page_html.Accessor();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID);

                    return accesser;
                }));
            }

            accessorList.Add(new Func<byte[], APIs.IAccessor>((byte[] data) =>
            {
                var parser = new APIs.video_page_html.Parser();

                if (data != null)
                    htmlCache.Value = parser.Parse(parser.Parse(data));

                var accesser = new APIs.tag_edit.UploadAccessor();
                accesser.Setting(
                    context.CookieContainer,
                    target.ID,
                    "json",
                    cmd,
                    EditTag.Name,
                    htmlCache.Value.context.csrfToken,
                    htmlCache.Value.context.watchAuthKey,
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
            videoCache.Clear();
            htmlCache.Clear();
        }
    }
}
