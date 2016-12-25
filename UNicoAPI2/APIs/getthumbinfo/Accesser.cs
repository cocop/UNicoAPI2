using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.getthumbinfo
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
        string id = "";

        HttpWebRequest request;

        public void Setting(CookieContainer CookieContainer, string id)
        {
            cookieContainer = CookieContainer;
            this.id = id;
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
            request = (HttpWebRequest)WebRequest.Create(
                "http://ext.nicovideo.jp/api/getthumbinfo/" + id);

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;

            return request.GetResponseAsync();
        }
    }
}
