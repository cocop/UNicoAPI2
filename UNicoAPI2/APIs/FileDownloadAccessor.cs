using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs
{
    public class FileDownloadAccessor : IAccessor
    {
        public AccessorType Type
        {
            get
            {
                return AccessorType.Download;
            }
        }

        CookieContainer cookieContainer;
        string url = "";
        long position;
        long length;

        public void Setting(CookieContainer CookieContainer, string url, long position, long length)
        {
            cookieContainer = CookieContainer;
            this.url = url;
            this.position = position;
            this.length = length;
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
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;

            if (length != -1)
                request.Headers["Range"] = "bytes=" + position + "-" + length;

            return request.GetResponseAsync();
        }
    }
}
