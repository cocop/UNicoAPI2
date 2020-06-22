using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.login_page_html
{
    public class Accessor : IAccessor
    {
        public AccessorType Type { get { return AccessorType.Download; } }


        CookieContainer cookieContainer;

        public void Setting(CookieContainer CookieContainer)
        {
            cookieContainer = CookieContainer;
        }

        public byte[] GetUploadData()
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            throw new NotImplementedException();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(
                "https://account.nicovideo.jp/login");

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;

            return request.GetResponseAsync();
        }
    }
}
