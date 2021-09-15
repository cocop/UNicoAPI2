using System;
using UNicoAPI2.Connect;
using UNicoAPI2.VideoService.Mylist;
using UNicoAPI2.VideoService.Series;
using UNicoAPI2.VideoService.User;
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

        /// <summary>マイリストへアクセスするページを取得する</summary>
        /// <param name="Target">ターゲットマイリスト</param>
        public SeriesPage GetSeriesPage(Series.Series Target)
        {
            if (Target.seriesPage != null)
                return Target.seriesPage;
            else
                return Target.seriesPage = new SeriesPage(Target, context);
        }

        /// <summary>ユーザーへアクセスするページを取得する</summary>
        /// <param name="Target">ターゲットユーザー</param>
        public UserPage GetUserPage(User.User Target)
        {
            if (Target.userPage != null)
                return Target.userPage;
            else
                return Target.userPage = new UserPage(Target, context);
        }

        /// <summary>動画を検索する</summary>
        /// <param name="Keyword">検索キーワード</param>
        /// <param name="SearchPage">検索ページの指定、1～nの間の数値を指定する</param>
        /// <param name="SearchType">検索方法を指定する</param>
        /// <param name="SearchOption">検索オプションを指定する、Filterメンバは無効</param>
        public Session<Response<VideoInfo[]>> Search(
            string Keyword,
            int SearchPage,
            SearchType SearchType,
            SearchOption SearchOption)
        {
            var session = new Session<Response<VideoInfo[]>>();

            session.SetAccessers(new Func<byte[], APIs.IAccessor>[]
            {
                (data) =>
                {
                    var accesser = new APIs.search_page_html.Accessor();
                    accesser.Setting(
                        context.CookieContainer,
                        SearchType.ToKey(),
                        Keyword,
                        SearchPage.ToString(),
                        SearchOption.SortOrder.ToKey(),
                        SearchOption.SortTarget.ToKey());

                    return accesser;
                }
            },
            (data) =>
            {
                var parser = new APIs.search_page_html.Parser();
                return Converter.VideoInfoResponse(context, parser.Parse(parser.Parse(data)));
            });

            return session;
        }
    }
}
