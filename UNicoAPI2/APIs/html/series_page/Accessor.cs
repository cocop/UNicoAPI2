using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.html.series_page
{
    public class Accessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }
        public string SeriesId { get; set; }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(
                $"https://www.nicovideo.jp/series/{SeriesId}");

            request.Method = ContentMethod.Get;
            request.CookieContainer = CookieContainer;

            return request.GetResponseAsync();
        }
    }
}