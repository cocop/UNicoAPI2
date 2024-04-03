using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs
{
    public class FileDownloadAccessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }
        public string Url { get; set; }
        public long Position { get; set; }
        public long Length { get; set; }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(Url);

            request.Method = ContentMethod.Get;
            request.CookieContainer = CookieContainer;
            request.Headers["User-Agent"] = "UNicoAPI2 (https://github.com/cocop/UNicoAPI2)";

            if (Length != -1)
                request.Headers["Range"] = $"bytes={Position}-{Length}";

            return request.GetResponseAsync();
        }
    }
}
