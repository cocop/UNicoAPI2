using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.getpostkey
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
        string block_no = "";
        string thread = "";

        HttpWebRequest request;

        public void Setting(CookieContainer CookieContainer, string block_no, string thread)
        {
            cookieContainer = CookieContainer;
            this.block_no = block_no;
            this.thread = thread;
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
            request = (HttpWebRequest)WebRequest.Create(
                "http://flapi.nicovideo.jp/api/getpostkey/?yugi=&version_sub=2&device=1&block_no="
                + block_no
                + "&version=1&thread=" + thread);

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;

            return request.GetResponseAsync();
        }
    }
}
