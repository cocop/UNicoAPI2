using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.html.login_page
{
    public class Accessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(
                "https://account.nicovideo.jp/login");

            request.Method = ContentMethod.Get;
            request.CookieContainer = CookieContainer;
            request.Headers["User-Agent"] = "UNicoAPI2 (https://github.com/cocop/UNicoAPI2)";

            return request.GetResponseAsync();
        }
    }
}
