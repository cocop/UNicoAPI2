using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.html.series_page
{
    public class Accessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }
        public string SeriesId { get; set; }
        public string UserId { get; set; }
        public int Page { get; set; }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (UserId != null)
                ? (HttpWebRequest)WebRequest.Create($"https://www.nicovideo.jp/user/{UserId}/series/{SeriesId}?page={Page}")
                : (HttpWebRequest)WebRequest.Create($"https://www.nicovideo.jp/series/{SeriesId}");

            request.Method = ContentMethod.Get;
            request.CookieContainer = CookieContainer;
            request.Headers["User-Agent"] = "UNicoAPI2 (https://github.com/cocop/UNicoAPI2)";

            return request.GetResponseAsync();
        }
    }
}
