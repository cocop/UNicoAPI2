using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.mylitv2
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
        string id = "";
        int index;
        int count;

        HttpWebRequest request;

        public void Setting(CookieContainer CookieContainer, string id, int index, int count)
        {
            cookieContainer = CookieContainer;
            this.id = id;
            this.index = index;
            this.count = count;
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
                $"https://nvapi.nicovideo.jp/v2/mylists/{id}?pageSize={count}&page={index}");

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;
            request.Headers["X-Frontend-Id"] = "6";
            request.Headers["X-Frontend-Version"] = "6";

            return request.GetResponseAsync();
        }
    }
}