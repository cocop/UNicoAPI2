using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.login
{
    public class Accessor : IAccessorWithUploadData
    {
        public CookieContainer CookieContainer { get; set; }
        public string MailTel { get; set; }
        public string Password { get; set; }

        HttpWebRequest request;

        public byte[] GetUploadData()
        {
            return Encoding.UTF8.GetBytes(
                "mail_tel=" + MailTel + "&password=" + Password);
        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            request = (HttpWebRequest)WebRequest.Create("https://account.nicovideo.jp/api/v1/login");

            request.Method = ContentMethod.Post;
            request.ContentType = ContentType.Form;
            request.CookieContainer = CookieContainer;

            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
