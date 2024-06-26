using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.ms.download_comment
{
    public class Accessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }
        public string ThreadId { get; set; }
        /// <summary>
        /// //メッセージサーバー
        /// </summary>
        public string Ms { get; set; }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(
                Ms + "thread?version=20090904&thread=" + ThreadId + "&res_from=1");

            request.Method = ContentMethod.Get;
            request.CookieContainer = CookieContainer;
            request.Headers["User-Agent"] = "UNicoAPI2 (https://github.com/cocop/UNicoAPI2)";

            return request.GetResponseAsync();
        }
    }
}
