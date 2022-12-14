using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.mylitv2
{
    public class Accessor : IAccessor
    {
        public CookieContainer CcookieContainer { get; set; }
        public string Id { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }

        HttpWebRequest request;

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            request = (HttpWebRequest)WebRequest.Create(
                $"https://nvapi.nicovideo.jp/v2/mylists/{Id}?pageSize={Count}&page={Index}");

            request.Method = ContentMethod.Get;
            request.CookieContainer = CcookieContainer;
            request.Headers["X-Frontend-Id"] = "6";
            request.Headers["X-Frontend-Version"] = "6";

            return request.GetResponseAsync();
        }
    }
}