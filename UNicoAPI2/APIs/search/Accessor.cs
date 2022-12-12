using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.search
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
                $"http://ext.nicovideo.jp/api/search/{Type}/{Word}?mode&page={Page}&order={Order}&sort={Sort}");

            request.Method = ContentMethod.Get;
            request.CookieContainer = CookieContainer;

            return request.GetResponseAsync();
        }
    }
}
