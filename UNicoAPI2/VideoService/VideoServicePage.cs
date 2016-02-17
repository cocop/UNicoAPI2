using System;
using UNicoAPI2.Connect;
using UNicoAPI2.VideoService.Mylist;
using UNicoAPI2.VideoService.Video;

namespace UNicoAPI2.VideoService
{
    /******************************************/
    /// <summary>
    /// ニコニコ動画サービスへアクセスするページ
    /// </summary>
    /******************************************/
    public class VideoServicePage
    {
        Context context;

        internal VideoServicePage(Context Context)
        {
            context = Context;
        }

        /// <summary>動画へアクセスするページを取得する</summary>
        /// <param name="Target">ターゲット動画</param>
        public VideoPage GetVideoPage(VideoInfo Target)
        {
            if (Target.videoPage != null)
                return Target.videoPage;
            else
                return Target.videoPage = new VideoPage(Target, context);
        }

        /// <summary>マイリストへアクセスするページを取得する</summary>
        /// <param name="Target">ターゲットマイリスト</param>
        public MylistPage GetMylistPage(Mylist.Mylist Target)
        {
            if (Target.mylistPage != null)
                return Target.mylistPage;
            else
                return Target.mylistPage = new MylistPage(Target, context);
        }

        /// <summary>動画を検索する</summary>
        /// <param name="Keyword">検索キーワード</param>
        /// <param name="SearchPage">検索ページの指定、1～nの間の数値を指定する</param>
        /// <param name="SearchType">検索方法を指定する</param>
        /// <param name="SearchOption">検索オプションを指定する</param>
        public Session<Response<VideoInfo[]>> Search(
            string Keyword,
            int SearchPage,
            SearchType SearchType,
            SearchOption SearchOption)
        {
            var session = new Session<Response<VideoInfo[]>>();

            session.SetAccessers(new Func<byte[], APIs.IAccesser>[]
            {
                (data) =>
                {
                    var accesser = new APIs.search.Accesser();
                    accesser.Setting(
                        context.CookieContainer,
                        SearchType.ToKey(),
                        Keyword,
                        SearchPage.ToString(),
                        SearchOption.SortOrder.ToKey(),
                        SearchOption.SortTarget.ToKey(),
                        SearchOption.PostTimeFilter.ToKey(),
                        SearchOption.PlayTimeFilter.ToKey());

                    return accesser;
                }
            },
            (data) =>
                Converter.VideoInfoResponse(context, new APIs.search.Parser().Parse(data)));

            return session;
        }
    }
}
