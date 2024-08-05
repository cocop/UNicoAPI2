using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.dmc.media_session
{
    /// <summary>
    /// レスポンスはHeartBeats
    /// </summary>
    public class Accessor : IAccessorWithUploadData
    {
        public CookieContainer CookieContainer { get; set; }
        public html.video_page.Response.Rootobject.Data.Response.Media MediaInfo { get; set; }

        HttpWebRequest request;

        public byte[] GetUploadData()
        {
            var data = new Request.Rootobject
            {
                session = new Request.Session()
                {
                    client_info = new Request.Client_Info
                    {
                        player_id = MediaInfo.delivery.movie.session.playerId
                    },
                    content_auth = new Request.Content_Auth
                    {
                        auth_type = MediaInfo.delivery.movie.session.authTypes.http,
                        content_key_timeout = MediaInfo.delivery.movie.session.contentKeyTimeout,
                        service_id = "nicovideo",
                        service_user_id = MediaInfo.delivery.movie.session.serviceUserId,
                    },
                    content_id = MediaInfo.delivery.movie.session.contentId,
                    content_src_id_sets = new Request.Content_Src_Id_Sets[]
                    {
                        new Request.Content_Src_Id_Sets()
                        {
                            content_src_ids = CreateContentSrcIds(MediaInfo.delivery.movie.session)
                        }
                    },
                    content_type = "movie",
                    content_uri = "",
                    keep_method = new Request.Keep_Method()
                    {
                        heartbeat = new Request.Heartbeat()
                        {
                            lifetime = MediaInfo.delivery.movie.session.heartbeatLifetime,
                        }
                    },
                    priority = MediaInfo.delivery.movie.session.priority,
                    protocol = new Request.Protocol()
                    {
                        name = "http",
                        parameters = new Request.Parameters()
                        {
                            http_parameters = new Request.Http_Parameters()
                            {
                                parameters = new Request.Parameters1()
                                {
                                    hls_parameters = new Request.Hls_Parameters()
                                    {
                                        segment_duration = 6000,
                                        transfer_preset = "",
                                        use_ssl = "yes",
                                        use_well_known_port = "yes",
                                    }
                                }
                            }
                        }
                    },
                    recipe_id = MediaInfo.delivery.movie.session.recipeId,
                    session_operation_auth = new Request.Session_Operation_Auth()
                    {
                        session_operation_auth_by_signature = new Request.Session_Operation_Auth_By_Signature()
                        {
                            signature = MediaInfo.delivery.movie.session.signature,
                            token = MediaInfo.delivery.movie.session.token,
                        }
                    },
                    timing_constraint = "unlimited",
                }
            };

            using (MemoryStream memStream = new MemoryStream())
            {
                var serialize = new DataContractJsonSerializer(typeof(Request.Rootobject));
                serialize.WriteObject(memStream, data);
                return memStream.ToArray();
            };
        }

        private Request.Content_Src_Ids[] CreateContentSrcIds(html.video_page.Response.Rootobject.Data.Response.Media.Delivery.Movie.Session sessionInfo)
        {
            var contentSrcIds = new List<Request.Content_Src_Ids>();

            for (int audioIndex = 0; audioIndex < sessionInfo.audios.Length; audioIndex++)
            {
                for (int videoIndex = 0; videoIndex < sessionInfo.videos.Length - 1; videoIndex++)
                {
                    contentSrcIds.Add(
                        new Request.Content_Src_Ids()
                        {
                            src_id_to_mux = new Request.Src_Id_To_Mux()
                            {
                                audio_src_ids = new string[]
                                {
                                    sessionInfo.audios[audioIndex]
                                },
                                video_src_ids = sessionInfo.videos.Skip(videoIndex).ToArray(),
                            }
                        });
                }
            }

            return contentSrcIds.ToArray();
        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            request = (HttpWebRequest)WebRequest.Create(MediaInfo.delivery.movie.session.urls[0].url + "?_format=json");
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
