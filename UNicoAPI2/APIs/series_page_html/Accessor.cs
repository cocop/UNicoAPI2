using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.series_page_html
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
        string series_id = "";

        public void Setting(CookieContainer CookieContainer, string series_id)
        {
            cookieContainer = CookieContainer;
            this.series_id = series_id;
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
                "https://www.nicovideo.jp/series" + series_id);

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;

            return request.GetResponseAsync();
        }
    }
}
