using System.Linq;

namespace UNicoAPI2.VideoService.Converter.SeriesPage
{
    public class DownloadSeries
    {
        public static Response<Series.Series> From(Context Context, APIs.html.series_page.Response.Rootobject seriesInfo, string SeriesID)
        {
            var result = Response.From<Series.Series>(200, null);

            result.Result = Context.IDContainer.GetSeries(SeriesID);
            result.Result.ID = SeriesID;
            result.Result.Title = seriesInfo.nvapi[0].body.data.detail.title;
            result.Result.User = Context.IDContainer.GetUser(seriesInfo.state.userDetails.userDetails.user.id);
            result.Result.User.Name = seriesInfo.state.userDetails.userDetails.user.nickname;
            result.Result.VideoList = seriesInfo.nvapi[0].body.data.items?.Select((video) =>
            {
                var videoInfo = Context.IDContainer.GetVideoInfo(video.video.id);
                videoInfo.Title = video.video.title;
                videoInfo.Thumbnail = new Picture(video.video.thumbnail.url, Context.CookieContainer);
                videoInfo.User = result.Result.User;
                return videoInfo;
            }).ToArray();

            return result;
        }
    }
}
