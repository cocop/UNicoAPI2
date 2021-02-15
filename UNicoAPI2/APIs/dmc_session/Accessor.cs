using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using UNicoAPI2.Connect;
using UNicoAPI2.APIs.dmc_session.Request;

namespace UNicoAPI2.APIs.dmc_session
{
    /// <summary>
    /// レスポンスはHeartBeats
    /// </summary>
    public class Accessor : IAccessor
    {
        public AccessorType Type => AccessorType.Upload;

        CookieContainer cookieContainer;
        video_page_html.Response.Rootobject.Video.Dmcinfo dmcInfo;
        HttpWebRequest request;

        public void Setting(CookieContainer CookieContainer, video_page_html.Response.Rootobject.Video.Dmcinfo dmcInfo)
        {
            cookieContainer = CookieContainer;
            this.dmcInfo = dmcInfo;
        }

        public byte[] GetUploadData()
        {
            var data = new Rootobject
            {
                session = new Session()
                {
                    client_info = new Client_Info
                    {
                        player_id = dmcInfo.session_api.player_id
                    },
                    content_auth = new Content_Auth
                    {
                        auth_type = dmcInfo.session_api.auth_types.http,
                        content_key_timeout = dmcInfo.session_api.content_key_timeout,
                        service_id = "nicovideo",
                        service_user_id = dmcInfo.user.user_id,
                    },
                    content_id = dmcInfo.session_api.content_id,
                    content_src_id_sets = new Content_Src_Id_Sets[]
                    {
                        new Content_Src_Id_Sets()
                        {
                            content_src_ids = CreateContent_Src_Ids(dmcInfo.session_api)
                        }
                    },
                    content_type = "movie",
                    content_uri = "",
                    keep_method = new Keep_Method()
                    {
                        heartbeat = new Heartbeat()
                        {
                            lifetime = dmcInfo.session_api.heartbeat_lifetime,
                        }
                    },
                    priority = dmcInfo.session_api.priority,
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
                    recipe_id = dmcInfo.session_api.recipe_id,
                    session_operation_auth = new Session_Operation_Auth()
                    {
                        session_operation_auth_by_signature = new Session_Operation_Auth_By_Signature()
                        {
                            signature = dmcInfo.session_api.signature,
                            token = dmcInfo.session_api.token,
                        }
                    },
                    timing_constraint = "unlimited",
                }
            };

            var serialize = new DataContractJsonSerializer(typeof(Rootobject));
            var memStream = new MemoryStream();
            serialize.WriteObject(memStream, data);

            return memStream.ToArray();
        }

        private Content_Src_Ids[] CreateContent_Src_Ids(video_page_html.Response.Rootobject.Video.Dmcinfo.Session_Api session_api)
        {
            var contentSrcIds = new List<Content_Src_Ids>();

            for (int audioIndex = 0; audioIndex < session_api.audios.Length; audioIndex++)
            {
                for (int videoIndex = 0; videoIndex < session_api.videos.Length - 1; videoIndex++)
                {
                    contentSrcIds.Add(
                        new Content_Src_Ids()
                        {
                            src_id_to_mux = new Src_Id_To_Mux()
                            {
                                audio_src_ids = new string[]
                                {
                                    session_api.audios[audioIndex]
                                },
                                video_src_ids = session_api.videos.Skip(videoIndex).ToArray(),
                            }
                        });
                }
            }

            return contentSrcIds.ToArray();
        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            var urls = dmcInfo?.session_api?.urls;
            if ((urls?.Length ?? 0) == 0)
                throw new System.Exception("dmcInfoの取得に失敗しました");

            request = (HttpWebRequest)WebRequest.Create(urls[0].url + "?_format=json");
            request.Accept = ContentType.Json;
            request.ContentType = ContentType.Json;
            request.Method = ContentMethod.Post;
            request.CookieContainer = cookieContainer;

            request.Headers["Content-Length"] = DataLength.ToString();
            request.Headers["Connection"] = "keep-alive";
            //request.Headers["Referer"] = "";

            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
