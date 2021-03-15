using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using UNicoAPI2.Connect;
using UNicoAPI2.APIs.media_session.Request;
using System.Text;

namespace UNicoAPI2.APIs.media_session
{
    /// <summary>
    /// レスポンスはHeartBeats
    /// </summary>
    public class Accessor : IAccessor
    {
        public AccessorType Type => AccessorType.Upload;

        CookieContainer cookieContainer;
        video_page_html.Response.Rootobject.Media mediaInfo;
        HttpWebRequest request;

        public void Setting(CookieContainer CookieContainer, video_page_html.Response.Rootobject.Media mediaInfo)
        {
            cookieContainer = CookieContainer;
            this.mediaInfo = mediaInfo;
        }

        public byte[] GetUploadData()
        {
            var data = new Rootobject
            {
                session = new Session()
                {
                    client_info = new Client_Info
                    {
                        player_id = mediaInfo.delivery.movie.session.playerId
                    },
                    content_auth = new Content_Auth
                    {
                        auth_type = mediaInfo.delivery.movie.session.authTypes.http,
                        content_key_timeout = mediaInfo.delivery.movie.session.contentKeyTimeout,
                        service_id = "nicovideo",
                        service_user_id = mediaInfo.delivery.movie.session.serviceUserId,
                    },
                    content_id = mediaInfo.delivery.movie.session.contentId,
                    content_src_id_sets = new Content_Src_Id_Sets[]
                    {
                        new Content_Src_Id_Sets()
                        {
                            content_src_ids = CreateContent_Src_Ids(mediaInfo.delivery.movie.session)
                        }
                    },
                    content_type = "movie",
                    content_uri = "",
                    keep_method = new Keep_Method()
                    {
                        heartbeat = new Heartbeat()
                        {
                            lifetime = mediaInfo.delivery.movie.session.heartbeatLifetime,
                        }
                    },
                    priority = mediaInfo.delivery.movie.session.priority,
                    protocol = new Protocol()
                    {
                        name = "http",
                        parameters = new Parameters()
                        {
                            http_parameters = new Http_Parameters()
                            {
                                parameters = new Parameters1()
                                {
                                    hls_parameters = new Hls_Parameters()
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
                    recipe_id = mediaInfo.delivery.movie.session.recipeId,
                    session_operation_auth = new Session_Operation_Auth()
                    {
                        session_operation_auth_by_signature = new Session_Operation_Auth_By_Signature()
                        {
                            signature = mediaInfo.delivery.movie.session.signature,
                            token = mediaInfo.delivery.movie.session.token,
                        }
                    },
                    timing_constraint = "unlimited",
                }
            };

            using (MemoryStream memStream = new MemoryStream())
            {
                var serialize = new DataContractJsonSerializer(typeof(Rootobject));
                serialize.WriteObject(memStream, data);
                return memStream.ToArray();
            };
        }

        private Content_Src_Ids[] CreateContent_Src_Ids(video_page_html.Response.Rootobject.Media.Delivery.Movie.Session sessionInfo)
        {
            var contentSrcIds = new List<Content_Src_Ids>();

            for (int audioIndex = 0; audioIndex < sessionInfo.audios.Length; audioIndex++)
            {
                for (int videoIndex = 0; videoIndex < sessionInfo.videos.Length - 1; videoIndex++)
                {
                    contentSrcIds.Add(
                        new Content_Src_Ids()
                        {
                            src_id_to_mux = new Src_Id_To_Mux()
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
            request = (HttpWebRequest)WebRequest.Create(mediaInfo.delivery.movie.session.urls[0].url + "?_format=json");
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
