using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.getvideo
{
    public class Accesser : IAccesser
    {
        public AccesserType Type
        {
            get
            {
                return AccesserType.Download;
            }
        }

        CookieContainer cookieContainer;
        string url = "";

        public void Setting(CookieContainer CookieContainer, string url)
        {
            cookieContainer = CookieContainer;
            this.url = url;
        }

        public byte[] GetUploadData()
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetUploadStreamAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;

            return request.GetResponseAsync();
        }
    }
}
