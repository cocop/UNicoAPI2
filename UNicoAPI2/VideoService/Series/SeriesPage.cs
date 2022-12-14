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
            return new Session<Response<Series>>((flow) =>
            {
                flow.Return(new APIs.series_page_html.Accessor
                {
                    CookieContainer = context.CookieContainer,
                    SeriesId = target.ID
                });

                var parser = new APIs.series_page_html.Parser();
                return Converter.SeriesPage.DownloadSeries.From(
                    context,
                    parser.Parse(parser.Parse(flow.GetResult())),
                    target.ID);
            });
        }
    }
}
