using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.nvapi.user_series
{
    public class Accessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }
        public string UserId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        HttpWebRequest request;

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            request = (HttpWebRequest)WebRequest.Create(
                $"https://nvapi.nicovideo.jp/v1/users/{UserId}/series?page={Page}&pageSize={PageSize}");

            request.Method = ContentMethod.Get;
            request.CookieContainer = CookieContainer;
            request.Headers["X-Frontend-Id"] = "6";
            request.Headers["X-Frontend-Version"] = "0";

            return request.GetResponseAsync();
        }
    }
}