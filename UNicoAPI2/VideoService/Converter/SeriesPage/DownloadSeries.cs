using System.Linq;

namespace UNicoAPI2.VideoService.Converter.SeriesPage
{
    public class DownloadSeries
    {
        public static Response<Series.Series> From(Context Context, APIs.html.series_page.Response.Result Response, string SeriesID)
        {
            var result = Converter.Response.From<Series.Series>(200, null);

            result.Result = Context.IDContainer.GetSeries(SeriesID);
            result.Result.ID = SeriesID;
            result.Result.Title = Response.Title;
            result.Result.User = Context.IDContainer.GetUser(Response.PostUser.ID);
            result.Result.User.Name = Response.PostUser?.Name;
            result.Result.OtherSeriesList = Response.OtherSeriesList?.Select((otherSeries) =>
            {
                var series = Context.IDContainer.GetSeries(otherSeries.ID);
                series.Title = otherSeries.Title;
                return series;
            }).ToArray();
            result.Result.VideoList = Response.VideoList?.Select((video) =>
            {
                var videoInfo = Context.IDContainer.GetVideoInfo(video.ID);
                videoInfo.Title = video.Title;
                videoInfo.Thumbnail = new Picture(video.ThumbnailUrl, Context.CookieContainer);
                videoInfo.User = result.Result.User;
                return videoInfo;
            }).ToArray();

            return result;
        }
    }
}
