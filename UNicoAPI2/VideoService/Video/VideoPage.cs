using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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

            switch (DownloadVideoInfoUseAPI)
            {
                case DownloadVideoInfoUseAPI.html:
                    return new Session<Response<VideoInfo>>((flow) =>
                    {
                        if (!htmlCache.IsAvailab)
                        {
                            var accesser = new APIs.video_page_html.Accessor();
                            accesser.Setting(
                                context.CookieContainer,
                                target.ID);
                            flow.Return(accesser);

                            var parser = new APIs.video_page_html.Parser();
                            htmlCache.Value = parser.Parse(parser.Parse(flow.GetResult()));
                        }

                        return Converter.VideoInfoResponse(
                            context,
                            htmlCache);
                    });
                case DownloadVideoInfoUseAPI.getthumbinfo:
                    return new Session<Response<VideoInfo>>((flow) =>
                    {
                        var accesser = new APIs.getthumbinfo.Accessor();
                        accesser.Setting(
                            context.CookieContainer,
                            target.ID);
                        flow.Return(accesser);

                        return Converter.VideoInfoResponse(
                            context,
                            new APIs.getthumbinfo.Parser().Parse(flow.GetResult()));
                    });
            }

            throw new Exception();
        }

        /// <summary>
        /// html5APIを使用して動画を取得する
        /// </summary>
        /// <returns>接続維持セッションを持ったVideoSource</returns>
        public Session<DmcVideoSource> GetDmcVideoSource()
        {
            return new Session<DmcVideoSource>((flow) =>
            {
                if (!htmlCache.IsAvailab || !isAvailabDmcCash || DateTime.Now > (htmlCache.GotTime?.AddSeconds(30) ?? DateTime.MinValue))
                {
                    var accesser = new APIs.video_page_html.Accessor();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID);

                    flow.Return(accesser);

                    var parser = new APIs.video_page_html.Parser();
                    htmlCache.Value = parser.Parse(parser.Parse(flow.GetResult()));
                }
                isAvailabDmcCash = false;

                {
                    var accesser = new APIs.media_session.Accessor();
                    accesser.Setting(
                        context.CookieContainer,
                        htmlCache.Value.media);

                    flow.Return(accesser);
                }

                {
                    var accesser = new APIs.media_session.Accessor();
                    accesser.Setting(
                        context.CookieContainer,
                        htmlCache.Value.media);

                    flow.Return(accesser);
                }

                {
                    var parser = new APIs.heartbeats.Parser();
                    return new DmcVideoSource(
                        context.CookieContainer,
                        new Uri(htmlCache.Value.media.delivery.movie.session.urls[0].url),
                        parser.Parse(flow.GetResult()));
                }
            });
        }

        /// <summary>
        /// いいねします
        /// </summary>
        /// <returns>いいねメッセージ</returns>
        public Session<Response<string>> DoLike()
        {
            return new Session<Response<string>>((flow) =>
            {
                var accessor = new APIs.likes.DoAccessor();
                accessor.Setting(context.CookieContainer, target.ID);
                flow.Return(accessor);

                return new Response<string>()
                {
                    Status = Status.OK,
                    Result = new APIs.likes.DoParser().Parse(flow.GetResult())?.data?.thanksMessage ?? ""
                };
            });
        }

        /// <summary>
        /// いいねを解除します
        /// </summary>
        public Session<Response> UndoLike()
        {
            return new Session<Response>((flow) =>
            {
                var accessor = new APIs.likes.UndoAccessor();
                accessor.Setting(context.CookieContainer, target.ID);
                flow.Return(accessor);

                return new Response()
                {
                    Status = Status.OK
                };
            });
        }

        /// <summary>
        /// コメントをアップロードする
        /// </summary>
        /// <param name="Comment">投稿するコメント</param>
        public Session<Response> UploadComment(Comment Comment)
        {
            return new Session<Response>((flow) =>
            {
                if (!htmlCache.IsAvailab)
                {
                    var accessor = new APIs.video_page_html.Accessor();
                    accessor.Setting(
                        context.CookieContainer,
                        target.ID);
                    flow.Return(accessor);

                    var parser = new APIs.video_page_html.Parser();
                    htmlCache.Value = parser.Parse(parser.Parse(flow.GetResult()));
                }

                if (postkey == "")
                {
                    var accessor = new APIs.getpostkey.Accessor();
                    accessor.Setting(
                        context.CookieContainer,
                        block_no,
                        htmlCache.Value.comment.threads[0].id.ToString());
                    flow.Return(accessor);

                    postkey = new APIs.getpostkey.Parser().Parse(flow.GetResult());
                }

                {
                    var accessor = new APIs.upload_comment.Accessor();
                    accessor.Setting(
                        context.CookieContainer,
                        htmlCache.Value.comment.server.url,
                        htmlCache.Value.comment.threads[0].id.ToString(),
                        ((int)(Comment.PlayTime.TotalMilliseconds / 10)).ToString(),
                        Comment.Command,
                        ticket,
                        htmlCache.Value.viewer.id.ToString(),
                        postkey,
                        Comment.Body);
                    flow.Return(accessor);
                }

                return Converter.Response(new APIs.upload_comment.Parser().Parse(flow.GetResult()));
            });
        }

        /// <summary>
        /// コメントをダウンロードする
        /// </summary>
        public Session<Response<Comment[]>> DownloadComment()
        {
            return new Session<Response<Comment[]>>((flow) =>
            {
                if (!htmlCache.IsAvailab)
                {
                    var accessor = new APIs.video_page_html.Accessor();
                    accessor.Setting(
                        context.CookieContainer,
                        target.ID);
                    flow.Return(accessor);

                    var parser = new APIs.video_page_html.Parser();
                    htmlCache.Value = parser.Parse(parser.Parse(flow.GetResult()));
                }

                {
                    var accessor = new APIs.download_comment.Accessor();
                    accessor.Setting(
                        context.CookieContainer,
                        htmlCache.Value.comment.server.url,
                        htmlCache.Value.comment.threads[0].id.ToString());
                    flow.Return(accessor);
                }

                {
                    var Serial = new APIs.download_comment.Parser().Parse(flow.GetResult());
                    ticket = Serial.thread[0].ticket;
                    block_no = ((Serial.thread[0].last_res + 1) / 100).ToString();

                    return Converter.CommentResponse(Serial);
                }
            });
        }

        /// <summary>
        /// キャッシュを削除する
        /// </summary>
        public void ClearCache()
        {
            htmlCache.Clear();
        }
    }
}
