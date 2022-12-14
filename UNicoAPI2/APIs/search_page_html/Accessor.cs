using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.search_page_html
{
    public class Accessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }
        public string Type { get; set; }
        public string Word { get; set; }
        public string Page { get; set; }
        public string Order { get; set; }
        public string Sort { get; set; }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(
                $"https://www.nicovideo.jp/{Type}/{Word}?page={Page}&sort={Sort}&order={Order}");

            request.Method = ContentMethod.Get;
            request.CookieContainer = CookieContainer;

            return request.GetResponseAsync();
        }
    }
}
