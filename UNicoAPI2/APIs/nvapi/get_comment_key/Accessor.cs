using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.nvapi.get_comment_key
{
    public class Accessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }
        public string ThreadId { get; set; }

        HttpWebRequest request;

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            request = (HttpWebRequest)WebRequest.Create($"https://nvapi.nicovideo.jp/v1/comment/keys/post?threadId={ThreadId}");

            request.Method = ContentMethod.Get;
            request.ContentType = ContentType.Json;
            request.CookieContainer = CookieContainer;
            request.Headers["x-frontend-id"] = "6";
            request.Headers["x-frontend-version"] = "0";

            return request.GetResponseAsync();
        }
    }
}
