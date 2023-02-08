using System;
using System.Linq;
using UNicoAPI2.APIs.nvapi.user_series.Response;

namespace UNicoAPI2.VideoService.Converter.SeriesPage
{
    public class DownloadOtherSeries
    {
        public static Response<Series.Series[]> From(Context context, Rootobject rootobject)
        {
            var result = Response.From<Series.Series[]>(rootobject.meta.status, rootobject.meta.message);

            result.Result = rootobject.data.items.Select((value) =>
            {
                var series = context.IDContainer.GetSeries(value.id);
                series.Title = value.title;
                series.User = context.IDContainer.GetUser(value.owner.id);
                return series;
            }).ToArray();

            return result;
        }
    }
}