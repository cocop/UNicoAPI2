using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.ms.upload_comment
{
    public class Accessor : IAccessorWithUploadData
    {
        public CookieContainer CookieContainer { get; set; }
        public string Ms { get; set; }
        public string Thread { get; set; }
        public string Vpos { get; set; }
        public string Mail { get; set; }
        public string Ticket { get; set; }
        public string UserId { get; set; }
        public string PostKey { get; set; }
        public string Body { get; set; }

        HttpWebRequest request;

        public byte[] GetUploadData()
        {
            return Encoding.UTF8.GetBytes(
                "<chat thread=\"" + Thread
                + "\" vpos=\"" + Vpos
                + "\" mail=\"" + Mail
                + "\" ticket=\"" + Ticket
                + "\" user_id=\"" + UserId
                + "\" postkey=\"" + PostKey
                + "\">" + Body + "</chat>");
        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            request = (HttpWebRequest)WebRequest.Create(Ms);

            request.Method = ContentMethod.Post;
            request.ContentType = ContentType.Xml;
            request.CookieContainer = CookieContainer;

            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
