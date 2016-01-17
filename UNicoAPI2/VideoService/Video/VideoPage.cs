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
                    videoCache = new APIs.getflv.Parser().Parse(data);

                    var accesser = new APIs.videopagehtml.Accesser();
                    accesser.Setting(
                        context.CookieContainer,
                        target.ID);

                    return accesser;
                },
                (data) =>
                {
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
    }
}
