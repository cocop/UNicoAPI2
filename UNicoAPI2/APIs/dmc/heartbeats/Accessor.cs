using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using UNicoAPI2.APIs.dmc.heartbeats.Response;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.dmc.heartbeats
{
    public class Accessor : IAccessorWithUploadData
    {
        public CookieContainer CookieContainer { get; set; }
        public Uri HeartBeatsUri { get; set; }
        public Data Data { get; set; }

        HttpWebRequest request;

        public byte[] GetUploadData()
        {
            var serialize = new DataContractJsonSerializer(typeof(Data));
            var memStream = new MemoryStream();
            serialize.WriteObject(memStream, Data);

            return memStream.ToArray();
        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            request = (HttpWebRequest)WebRequest.Create(HeartBeatsUri + "/" + Data.session.id + "?_format=json&_method=PUT");
            request.Accept = ContentType.Json;
            request.ContentType = ContentType.Json;
            request.Method = ContentMethod.Post;
            request.CookieContainer = CookieContainer;

            request.Headers["Content-Length"] = DataLength.ToString();
            request.Headers["Connection"] = "keep-alive";
            request.Headers["User-Agent"] = "UNicoAPI2 (https://github.com/cocop/UNicoAPI2)";

            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
