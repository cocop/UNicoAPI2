using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.video_page_html
{
    public class Accessor : IAccessor
    {
        public AccessorType Type
        {
            get
            {
                return AccessorType.Download;
            }
        }

        CookieContainer cookieContainer;
        string video_id = "";

        public void Setting(CookieContainer CookieContainer, string video_id)
        {
            cookieContainer = CookieContainer;
            this.video_id = video_id;
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
                "http://www.nicovideo.jp/watch/" + video_id);

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;

            return request.GetResponseAsync();
        }
    }
}
