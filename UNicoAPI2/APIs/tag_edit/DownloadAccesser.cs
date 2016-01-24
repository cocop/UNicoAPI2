using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.tag_edit
{
    public class DownloadAccesser : IAccesser
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
        string res_type = "";
        string cmd = "";

        public void Setting(CookieContainer CookieContainer, string id, string res_type, string cmd)
        {
            cookieContainer = CookieContainer;
            this.id = id;
            this.res_type = res_type;
            this.cmd = cmd;
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
            var request = (HttpWebRequest)WebRequest.Create(
                "http://www.nicovideo.jp/tag_edit/" + id +
                "?res_type=" + res_type +
                "&cmd=" + cmd);

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;

            return request.GetResponseAsync();
        }
    }
}
