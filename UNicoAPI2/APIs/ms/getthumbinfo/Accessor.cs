using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.ms.getthumbinfo
{
    public class Accessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }
        public string Id { get; set; }

        HttpWebRequest request;

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            request = (HttpWebRequest)WebRequest.Create(
                "http://ext.nicovideo.jp/api/getthumbinfo/" + Id);

            request.Method = ContentMethod.Get;
            request.CookieContainer = CookieContainer;

            return request.GetResponseAsync();
        }
    }
}
