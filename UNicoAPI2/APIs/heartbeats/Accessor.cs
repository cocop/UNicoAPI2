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
        Uri heartBeatsUri;
        Response.Data data;
        HttpWebRequest request;

        public void Setting(CookieContainer CookieContainer, Uri heartBeatsUri, Response.Data data)
        {
            cookieContainer = CookieContainer;
            this.heartBeatsUri = heartBeatsUri;
            this.data = data;
        }

        public byte[] GetUploadData()
        {
            var serialize = new DataContractJsonSerializer(typeof(Response.Data));
            var memStream = new MemoryStream();
            serialize.WriteObject(memStream, data);

            return memStream.ToArray();
        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            request = (HttpWebRequest)WebRequest.Create(heartBeatsUri + "/" + data.session.id + "?_format=json&_method=PUT");
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
