using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.upload_comment
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
        string ms = "";
        string thread = "";
        string vpos = "";
        string mail = "";
        string ticket = "";
        string user_id = "";
        string postkey = "";
        string body = "";

        HttpWebRequest request;

        public void Setting(CookieContainer CookieContainer, string ms, string thread, string vpos, string mail, string ticket, string user_id, string postkey, string body)
        {
            cookieContainer = CookieContainer;
            this.ms = ms;
            this.thread = thread;
            this.vpos = vpos;
            this.mail = mail;
            this.ticket = ticket;
            this.user_id = user_id;
            this.postkey = postkey;
            this.body = body;
        }

        public byte[] GetUploadData()
        {
            return Encoding.UTF8.GetBytes(
                "<chat thread=\"" + thread
                + "\" vpos=\"" + vpos
                + "\" mail=\"" + mail
                + "\" ticket=\"" + ticket
                + "\" user_id=\"" + user_id
                + "\" postkey=\"" + postkey
                + "\">" + body + "</chat>");
        }

        public Task<Stream> GetUploadStreamAsync()
        {
            request = (HttpWebRequest)WebRequest.Create(ms);

            request.Method = ContentMethod.Post;
            request.ContentType = ContentType.Xml;
            request.CookieContainer = cookieContainer;

            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
