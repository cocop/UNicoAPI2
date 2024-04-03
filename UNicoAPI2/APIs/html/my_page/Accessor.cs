using System.Net;
using System.Threading.Tasks;

namespace UNicoAPI2.APIs.html.my_page
{
    public class Accessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }


        HttpWebRequest request;

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            request = (HttpWebRequest)WebRequest.Create("http://www.nicovideo.jp/my");
            request.Headers["User-Agent"] = "UNicoAPI2 (https://github.com/cocop/UNicoAPI2)";

            request.CookieContainer = CookieContainer;

            return request.GetResponseAsync();
        }
    }
}
