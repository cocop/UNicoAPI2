using System;
using UNicoAPI2.Connect;
using Windows.Media.Core;

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

        Cache<APIs.html.video_page.Response.Rootobject> htmlCache = new Cache<APIs.html.video_page.Response.Rootobject>();

        string postKey = null;

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

            switch (DownloadVideoInfoUseAPI)
            {
                case DownloadVideoInfoUseAPI.html:
                    return new Session<Response<VideoInfo>>((flow) =>
                    {
                        if (!htmlCache.IsAvailab)
                        {
                            flow.Return(new APIs.html.video_page.Accessor()
                            {
                                CookieContainer = context.CookieContainer,
                                VideoId = target.ID
                            });

                            var parser = new APIs.html.video_page.Parser();
                            htmlCache.Value = parser.Parse(parser.Parse(flow.GetResult()));
                        }

                        return Converter.VideoPage.DownloadVideoInfo.From(
                            context,
                            htmlCache);
                    });
                case DownloadVideoInfoUseAPI.getthumbinfo:
                    return new Session<Response<VideoInfo>>((flow) =>
                    {
                        flow.Return(new APIs.ms.getthumbinfo.Accessor()
                        {
                            CookieContainer = context.CookieContainer,
                            Id = target.ID
                        });

                        return Converter.VideoPage.DownloadVideoInfo.From(
                            context,
                            new APIs.ms.getthumbinfo.Parser().Parse(flow.GetResult()));
                    });
            }

            throw new Exception();
        }

        /// <summary>
        /// html5APIを使用して動画を取得する
        /// </summary>
        /// <returns>ContentURL</returns>
        public Session<Uri> GetVideoSource()
        {
            return new Session<Uri>((flow) =>
            {
                if (!htmlCache.IsAvailab || DateTime.Now > (htmlCache.GotTime?.AddSeconds(30) ?? DateTime.MinValue))
                {

                    flow.Return(new APIs.html.video_page.Accessor()
                    {
                        CookieContainer = context.CookieContainer,
                        VideoId = target.ID
                    });

                    var parser = new APIs.html.video_page.Parser();
                    htmlCache.Value = parser.Parse(parser.Parse(flow.GetResult()));
                }

                flow.Return(new APIs.nvapi.access_rights.Accessor()
                {
                    CookieContainer = context.CookieContainer,
                    videoId = target.ID,
                    actionTrackId = htmlCache.Value.data.response.client.watchTrackId,
                    domand = htmlCache.Value.data.response.media.domand,
                });

                {
                    var parser = new APIs.nvapi.access_rights.Parser();
                    var contentUrl = new Uri(parser.Parse(flow.GetResult()).data.contentUrl);
                    return contentUrl;
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
                flow.Return(new APIs.nvapi.likes.DoAccessor()
                {
                    CookieContainer = context.CookieContainer,
                    VideoId = target.ID
                });

                return new Response<string>()
                {
                    Status = Status.OK,
                    Result = new APIs.nvapi.likes.DoParser().Parse(flow.GetResult())?.data?.thanksMessage ?? ""
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
                flow.Return(new APIs.nvapi.likes.UndoAccessor()
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
                    flow.Return(new APIs.html.video_page.Accessor()
                    {
                        CookieContainer = context.CookieContainer,
                        VideoId = target.ID
                    });

                    var parser = new APIs.html.video_page.Parser();
                    htmlCache.Value = parser.Parse(parser.Parse(flow.GetResult()));
                }

                var postTarget = Array.Find(htmlCache.Value.data.response.comment.nvComment._params.targets, (i) => i.fork == "main");

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
                    flow.Return(new APIs.html.video_page.Accessor()
                    {
                        CookieContainer = context.CookieContainer,
                        VideoId = target.ID
                    });

                    var parser = new APIs.html.video_page.Parser();
                    htmlCache.Value = parser.Parse(parser.Parse(flow.GetResult()));
                }

                flow.Return(new APIs.nvcomment.get.Accessor()
                {
                    CookieContainer = context.CookieContainer,
                    ThreadKey = htmlCache.Value.data.response.comment.nvComment.threadKey,
                    Target = Array.ConvertAll(
                        htmlCache.Value.data.response.comment.nvComment._params.targets,
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
