using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.tag_edit
{
    public class DownloadAccessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }
        public string Id { get; set; }
        public string ResType { get; set; }
        public string Cmd { get; set; }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(
                "http://www.nicovideo.jp/tag_edit/{Id}?res_type={ResType}&cmd={Cmd}");

            request.Method = ContentMethod.Get;
            request.CookieContainer = CookieContainer;

            return request.GetResponseAsync();
        }
    }
}
