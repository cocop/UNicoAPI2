using System;
using UNicoAPI2.APIs.html.series_page.Response;
using UNicoAPI2.Connect;

namespace UNicoAPI2.VideoService.Series
{
    /******************************************/
    /// <summary>
    /// シリーズページへアクセスする
    /// </summary>
    /******************************************/
    public class SeriesPage
    {
        Series target;
        Context context;
        Cache<Rootobject> infoCache;


        internal SeriesPage(Series Target, Context Context)
        {
            target = Target;
            context = Context;
            infoCache = new Cache<Rootobject>()
            {
                Deadline = TimeSpan.FromMinutes(30)
            };
        }

        /// <summary>
        /// シリーズを取得する
        /// </summary>
        public Session<Response<Series>> DownloadSeries()
        {
            return new Session<Response<Series>>((flow) =>
            {
                if (!infoCache.IsAvailab)
                {
                    flow.Return(new APIs.html.series_page.Accessor
                    {
                        CookieContainer = context.CookieContainer,
                        SeriesId = target.ID
                    });

                    var parser = new APIs.html.series_page.Parser();
                    infoCache.Value = parser.HtmlParse(flow.GetResult());
                }

                return Converter.SeriesPage.DownloadSeries.From(
                    context,
                    infoCache,
                    target.ID);
            });
        }

        /// <summary>
        /// 同じ投稿者の、別のシリーズ一覧を取得する
        /// </summary>
        public Session<Response<Series[]>> DownloadOtherSeries()
        {
            return new Session<Response<Series[]>>((flow) =>
            {
                if (!infoCache.IsAvailab)
                {
                    flow.Return(new APIs.html.series_page.Accessor
                    {
                        CookieContainer = context.CookieContainer,
                        SeriesId = target.ID
                    });

                    var parser = new APIs.html.series_page.Parser();
                    infoCache.Value = parser.HtmlParse(flow.GetResult());
                }

                {
                    flow.Return(new APIs.nvapi.user_series.Accessor
                    {
                        CookieContainer = context.CookieContainer,
                        UserId = infoCache.Value.state.userDetails.userDetails.user.id,
                        Page = 1,
                        PageSize = 100
                    });

                    var parser = new APIs.nvapi.user_series.Parser();
                    return Converter.SeriesPage.DownloadOtherSeries.From(
                        context,
                        parser.Parse(flow.GetResult()));
                }
            });
        }

        public void ClearCache()
        {
            infoCache.Clear();
        }
    }
}
