using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.getpostkey
{
    public class Accessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }
        public string BlockNo { get; set; }
        public string Thread { get; set; }

        HttpWebRequest request;

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
            request = (HttpWebRequest)WebRequest.Create(
                "https://flapi.nicovideo.jp/api/getpostkey/?version_sub=2&device=1&block_no="
                + BlockNo
                + "&version=1&thread=" + Thread);

            request.Method = ContentMethod.Get;
            request.CookieContainer = CookieContainer;

            return request.GetResponseAsync();
        }
    }
}
