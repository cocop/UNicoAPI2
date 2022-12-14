using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.likes
{
    public class UndoAccessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }
        public string VideoId { get; set; }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(
                "https://nvapi.nicovideo.jp/v1/users/me/likes/items?videoId=" + VideoId);

            request.Method = ContentMethod.Delete;
            request.CookieContainer = CookieContainer;

            return request.GetResponseAsync();
        }
    }
}
