using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.login
{
    public class Accesser : IAccesser
    {
        public AccesserType Type
        {
            get
            {
                return AccesserType.Upload;
            }
        }

        CookieContainer cookieContainer;
        string mail_tel = "";
        string password = "";

        HttpWebRequest request;

        public void Setting(CookieContainer CookieContainer, string mail_tel, string password)
        {
            cookieContainer = CookieContainer;
            this.mail_tel = mail_tel;
            this.password = password;
        }

        public byte[] GetUploadData()
        {
            return Encoding.UTF8.GetBytes(
                "mail_tel=" + mail_tel + "&password=" + password);
        }

        public Task<Stream> GetUploadStreamAsync()
        {
            request = (HttpWebRequest)WebRequest.Create("https://secure.nicovideo.jp/secure/login");

            request.Method = ContentMethod.Post;
            request.ContentType = ContentType.Form;
            request.CookieContainer = cookieContainer;

            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
