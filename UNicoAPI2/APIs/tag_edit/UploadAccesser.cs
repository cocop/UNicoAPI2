using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.tag_edit
{
    public class UploadAccesser : IAccesser
    {
        public AccesserType Type
        {
            get
            {
                return AccesserType.Upload;
            }
        }

        CookieContainer cookieContainer;
        HttpWebRequest request;
        string id = "";
        string res_type = "";
        string cmd = "";
        string tag = "";
        string token = "";
        string watch_auth_key = "";
        string owner_lock = "";

        public void Setting(CookieContainer CookieContainer, string id, string res_type, string cmd, string tag, string token, string watch_auth_key, string owner_lock)
        {
            cookieContainer = CookieContainer;
            this.id = id;
            this.res_type = res_type;
            this.cmd = cmd;
            this.tag = tag;
            this.token = token;
            this.watch_auth_key = watch_auth_key;
            this.owner_lock = owner_lock;
        }

        public byte[] GetUploadData()
        {
            return Encoding.UTF8.GetBytes(
                "res_type=" + res_type +
                "&cmd=" + cmd +
                "&tag=" + tag +
                "&id=undefined" +
                "&token=" + token +
                "&watch_auth_key=" + watch_auth_key +
                "&owner_lock=" + owner_lock);
        }

        public Task<Stream> GetUploadStreamAsync()
        {
            request = (HttpWebRequest)WebRequest.Create(
                "http://www.nicovideo.jp/tag_edit/" + id);

            request.Method = ContentMethod.Post;

            request.CookieContainer = cookieContainer;
            request.ContentType = ContentType.Form;
            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
