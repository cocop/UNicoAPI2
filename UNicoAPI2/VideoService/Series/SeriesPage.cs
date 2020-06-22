using System;
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

        internal SeriesPage(Series Target, Context Context)
        {
            target = Target;
            context = Context;
        }

        /// <summary>
        /// シリーズを取得する
        /// </summary>
        public Session<Response<Series>> DownloadSeries()
        {
            var session = new Session<Response<Series>>();

            session.SetAccessers(
                new Func<byte[], APIs.IAccessor>[]
                {
                    (data) =>
                    {
                        var accesser = new APIs.series_page_html.Accessor();
                        accesser.Setting(
                            context.CookieContainer,
                            target.ID);

                        return accesser;
                    }
                },
                (data) =>
                {
                    var parser = new APIs.series_page_html.Parser();
                    return Converter.SeriesResponse(
                        context,
                        parser.Parse(parser.Parse(data)),
                        target.ID);
                });

            return session;
        }
    }
}
