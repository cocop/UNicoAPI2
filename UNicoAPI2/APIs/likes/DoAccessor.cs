using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.likes
{
    public class DoAccessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }
        public string VideoId { get; set; }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(
                "https://nvapi.nicovideo.jp/v1/users/me/likes/items?videoId=" + VideoId);

            request.Method = ContentMethod.Post;
            request.CookieContainer = CookieContainer;

            return request.GetResponseAsync();
        }
    }
}
