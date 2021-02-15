using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using UNicoAPI2.Connect;
using System;

namespace UNicoAPI2.APIs.heartbeats
{
    public class Accessor : IAccessor
    {
        public AccessorType Type => AccessorType.Upload;

        CookieContainer cookieContainer;
        Response.Session session;
        HttpWebRequest request;

        public void Setting(CookieContainer CookieContainer, Response.Session session)
        {
            cookieContainer = CookieContainer;
            this.session = session;
        }

        public byte[] GetUploadData()
        {
            var serialize = new DataContractJsonSerializer(typeof(Response.Session));
            var memStream = new MemoryStream();
            serialize.WriteObject(memStream, session);

            return memStream.ToArray();
        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            request = (HttpWebRequest)WebRequest.Create(session.content_uri);
            request.Accept = ContentType.Json;
            request.ContentType = ContentType.Json;
            request.Method = ContentMethod.Post;
            request.CookieContainer = cookieContainer;

            request.Headers["Content-Length"] = DataLength.ToString();
            request.Headers["Connection"] = "keep-alive";

            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
