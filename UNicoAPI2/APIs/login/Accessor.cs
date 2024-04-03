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
            request = (HttpWebRequest)WebRequest.Create("https://account.nicovideo.jp/login/redirector?show_button_twitter=1&site=niconico&show_button_facebook=1&next_url=/my");

            request.Method = ContentMethod.Post;
            request.ContentType = ContentType.Form;
            request.CookieContainer = CookieContainer;

            request.Headers["Sec-Fetch-Dest"] = "document";
            request.Headers["Sec-Fetch-Mode"] = "navigate";
            request.Headers["Sec-Fetch-Site"] = "same-origin";
            request.Headers["Sec-Fetch-User"] = "?1";
            request.Headers["Upgrade-Insecure-Requests"] = "1";
            request.Headers["User-Agent"] = "UNicoAPI2 (https://github.com/cocop/UNicoAPI2)";

            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
