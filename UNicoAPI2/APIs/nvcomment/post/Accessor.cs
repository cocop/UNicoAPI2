using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.nvcomment.post
{
    public class Accessor : IAccessorWithUploadData
    {

        public CookieContainer CookieContainer { get; set; }
        public string ThreadId { get; set; }
        public string Body { get; set; }
        public string[] Commands { get; set; }
        public string PostKey { get; set; }
        public string VideoId { get; set; }
        public int VposMs { get; set; }

        HttpWebRequest request;


        public byte[] GetUploadData()
        {
            var data = new Request.Rootobject()
            {
                body = Body,
                commands = Commands,
                postKey = PostKey,
                videoId = VideoId,
                vposMs = VposMs
            };

            var str = JsonSerializer.Serialize(data, typeof(Request.Rootobject));
            return Encoding.UTF8.GetBytes(str);
        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            request = (HttpWebRequest)WebRequest.Create($"https://nvcomment.nicovideo.jp/v1/threads/{ThreadId}/comments");

            request.Method = ContentMethod.Post;
            request.CookieContainer = CookieContainer;
            request.Headers["Content-Length"] = DataLength.ToString();
            request.ContentType = ContentType.Json;
            request.Headers["x-frontend-id"] = "6";
            request.Headers["x-frontend-version"] = "0";

            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
