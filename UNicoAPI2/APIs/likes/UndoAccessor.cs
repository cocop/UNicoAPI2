using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.likes
{
    public class UndoAccessor : IAccessor
    {
        public AccessorType Type
        {
            get
            {
                return AccessorType.Download;
            }
        }

        CookieContainer cookieContainer;
        string videoId = "";

        public void Setting(CookieContainer CookieContainer, string videoId)
        {
            cookieContainer = CookieContainer;
            this.videoId = videoId;
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
                "https://nvapi.nicovideo.jp/v1/users/me/likes/items?videoId=" + videoId);

            request.Method = ContentMethod.Delete;
            request.CookieContainer = cookieContainer;

            return request.GetResponseAsync();
        }
    }
}
