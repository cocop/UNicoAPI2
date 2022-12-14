using System;
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
        string postKey = null;

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
                            flow.Return(new APIs.video_page_html.Accessor()
                            {
                                CookieContainer = context.CookieContainer,
                                VideoId = target.ID
                            });

                            var parser = new APIs.video_page_html.Parser();
                            htmlCache.Value = parser.Parse(parser.Parse(flow.GetResult()));
                        }

                        return Converter.VideoPage.DownloadVideoInfo.From(
                            context,
                            htmlCache);
                    });
                case DownloadVideoInfoUseAPI.getthumbinfo:
                    return new Session<Response<VideoInfo>>((flow) =>
                    {
                        flow.Return(new APIs.getthumbinfo.Accessor()
                        {
                            CookieContainer = context.CookieContainer,
                            Id = target.ID
                        });

                        return Converter.VideoPage.DownloadVideoInfo.From(
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

                    flow.Return(new APIs.video_page_html.Accessor()
                    {
                        CookieContainer = context.CookieContainer,
                        VideoId = target.ID
                    });

                    var parser = new APIs.video_page_html.Parser();
                    htmlCache.Value = parser.Parse(parser.Parse(flow.GetResult()));
                }
                isAvailabDmcCash = false;

                flow.Return(new APIs.media_session.Accessor()
                {
                    CookieContainer = context.CookieContainer,
                    MediaInfo = htmlCache.Value.media
                });

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
                flow.Return(new APIs.likes.DoAccessor()
                {
                    CookieContainer = context.CookieContainer,
                    VideoId = target.ID
                });

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
                flow.Return(new APIs.likes.UndoAccessor()
                {
                    CookieContainer = context.CookieContainer,
                    VideoId = target.ID
                });

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
                    flow.Return(new APIs.video_page_html.Accessor()
                    {
                        CookieContainer = context.CookieContainer,
                        VideoId = target.ID
                    });

                    var parser = new APIs.video_page_html.Parser();
                    htmlCache.Value = parser.Parse(parser.Parse(flow.GetResult()));
                }

                var postTarget = Array.Find(htmlCache.Value.comment.nvComment._params.targets, (i) => i.fork == "main");

                if (postKey == null)
                {
                    flow.Return(new APIs.nvapi.get_comment_key.Accessor()
                    {
                        CookieContainer = context.CookieContainer,
                        ThreadId = postTarget.id.ToString()
                    });

                    var res = new APIs.nvapi.get_comment_key.Parser().Parse(flow.GetResult());
                    if (!res.meta.status.ToString().StartsWith("2"))
                    {
                        return new Response()
                        {
                            Status = Status.UnknownError
                        };
                    }

                    postKey = res.data.postKey;
                }

                flow.Return(new APIs.nvcomment.post.Accessor()
                {
                    CookieContainer = context.CookieContainer,
                    ThreadId = postTarget.id.ToString(),
                    VideoId = target.ID,
                    Body = Comment.Body,
                    Commands = Comment?.Command?.Split() ?? new string[] { },
                    PostKey = postKey,
                    VposMs = (int)(Comment.PlayTime.TotalMilliseconds)
                });

                return Converter.VideoPage.UploadComment.From(new APIs.nvcomment.post.Parser().Parse(flow.GetResult()));
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
                    flow.Return(new APIs.video_page_html.Accessor()
                    {
                        CookieContainer = context.CookieContainer,
                        VideoId = target.ID
                    });

                    var parser = new APIs.video_page_html.Parser();
                    htmlCache.Value = parser.Parse(parser.Parse(flow.GetResult()));
                }

                flow.Return(new APIs.nvcomment.get.Accessor()
                {
                    CookieContainer = context.CookieContainer,
                    ThreadKey = htmlCache.Value.comment.nvComment.threadKey,
                    Target = Array.ConvertAll(
                        htmlCache.Value.comment.nvComment._params.targets,
                        (i) =>
                        {
                            return new APIs.nvcomment.get.Request.Target()
                            {
                                id = i.id.ToString(),
                                fork = i.fork
                            };
                        })
                });

                var serial = new APIs.nvcomment.get.Parser().Parse(flow.GetResult());
                return Converter.VideoPage.DownloadComment.From(serial);
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
