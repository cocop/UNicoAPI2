using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.get_download_comment
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
        string thread_id = "";
        string ms = "";//メッセージサーバー

        public void Setting(CookieContainer CookieContainer, string ms, string thread_id)
        {
            cookieContainer = CookieContainer;
            this.ms = ms;
            this.thread_id = thread_id;
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
                ms + "thread?version=20090904&thread=" + thread_id + "&res_from=1");

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;

            return request.GetResponseAsync();
        }
    }
}
