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

            request.CookieContainer = CookieContainer;
            request.Headers["Sec-Fetch-Dest"] = "document";
            request.Headers["Sec-Fetch-Mode"] = "navigate";
            request.Headers["Sec-Fetch-Site"] = "none";
            request.Headers["Sec-Fetch-User"] = "?1";
            request.Headers["Upgrade-Insecure-Requests"] = "1";

            return request.GetResponseAsync();
        }
    }
}
