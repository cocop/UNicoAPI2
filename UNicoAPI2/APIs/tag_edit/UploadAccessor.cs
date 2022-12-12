using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.tag_edit
{
    public class UploadAccessor : IAccessorWithUploadData
    {
        public CookieContainer CookieContainer { get; set; }
        public string Id { get; set; }
        public string ResType { get; set; }
        public string Cmd { get; set; }
        public string Tag { get; set; }
        public string Token { get; set; }
        public string WatchAuthKey { get; set; }
        public string OwnerLock { get; set; }

        HttpWebRequest request;

        public byte[] GetUploadData()
        {
            return Encoding.UTF8.GetBytes(
                "res_type=" + ResType +
                "&cmd=" + Cmd +
                "&tag=" + Tag +
                "&id=undefined" +
                "&token=" + Token +
                "&watch_auth_key=" + WatchAuthKey +
                "&owner_lock=" + OwnerLock);
        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            request = (HttpWebRequest)WebRequest.Create(
                $"http://www.nicovideo.jp/tag_edit/{Id}");

            request.Method = ContentMethod.Post;

            request.CookieContainer = CookieContainer;
            request.ContentType = ContentType.Form;
            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
