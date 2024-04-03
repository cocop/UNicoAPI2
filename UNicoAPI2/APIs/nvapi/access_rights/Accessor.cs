using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using UNicoAPI2.Connect;
using static UNicoAPI2.APIs.html.video_page.Response.Rootobject.Media;

namespace UNicoAPI2.APIs.nvapi.access_rights
{
    public class Accessor : IAccessorWithUploadData
    {
        public CookieContainer CookieContainer { get; set; }
        public string videoId { get; set; }
        public string actionTrackId { get; set; }
        public Domand domand { get; set; }

        HttpWebRequest request;

        private string[][] get_outputs()
        {
            var result = new List<string[]>();
            string selected_audio = null;

            foreach (var audio in domand.audios)
            {
                if (audio.isAvailable)
                {
                    selected_audio = audio.id;
                    break;
                }
            }


            foreach(var video in domand.videos)
            {
                if (!video.isAvailable)
                {
                    continue;
                }

                result.Add(new string[]
                {
                    video.id,
                    selected_audio,
                });

                break;
            }

            return result.ToArray();
        }

        public byte[] GetUploadData()
        {
            var serialize = new DataContractJsonSerializer(typeof(Request.Rootobject));
            var memStream = new MemoryStream();
            serialize.WriteObject(memStream, new Request.Rootobject
            {
                outputs = get_outputs(),
            });

            return memStream.ToArray();

        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            request = (HttpWebRequest)WebRequest.Create($"https://nvapi.nicovideo.jp/v1/watch/{videoId}/access-rights/hls?actionTrackId={actionTrackId}");
            request.Method = ContentMethod.Post;
            request.ContentType = ContentType.Json;
            request.CookieContainer = CookieContainer;
            request.Headers["X-Frontend-Id"] = "6";
            request.Headers["X-Frontend-Version"] = "0";
            request.Headers["X-Request-With"] = "https://www.nicovideo.jp";
            request.Headers["X-Access-Right-Key"] = domand.accessRightKey;
            request.Headers["Content-Length"] = DataLength.ToString();
            request.Headers["User-Agent"] = "UNicoAPI2 (https://github.com/cocop/UNicoAPI2)";

            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
