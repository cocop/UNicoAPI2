using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.dmc_session
{
    public class Accessor : IAccessor
    {
        public AccessorType Type => AccessorType.Upload;

        CookieContainer cookieContainer;
        video_page_html.Serial.Rootobject dmcObject;
        HttpWebRequest request;

        public void Setting(CookieContainer CookieContainer, video_page_html.Serial.Rootobject DmcObject)
        {
            cookieContainer = CookieContainer;
            dmcObject = DmcObject;
        }

        public byte[] GetUploadData()
        {
            /*
            var data = new Rootobject
            {
                session = new Session()
                {

                    client_info = new Client_Info
                    {
                        player_id = dmcObject.video.dmcInfo.session_api.player_id
                    },
                    content_auth = new Content_Auth
                    {
                        auth_type = dmcObject.video.dmcInfo.session_api.auth_types.http,
                        content_key_timeout = dmcObject.video.dmcInfo.session_api.content_key_timeout ?? 0,
                        service_id = "nicovideo",
                        service_user_id = dmcObject.video.dmcInfo.user?.user_id ?? "",
                    },
                    content_id = dmcObject.video.dmcInfo.session_api.content_id,
                    content_src_id_sets = new Content_Src_Id_Sets[]
                    {
                        new Content_Src_Id_Sets()
                        {
                            content_src_ids = new Content_Src_Ids[]
                            {
                                new Content_Src_Ids()
                                {
                                    src_id_to_mux = new Src_Id_To_Mux()
                                    {
                                        audio_src_ids = new string[]
                                        {
                                            dmcObject.video.dmcInfo.session_api.audios[0]
                                        },
                                        video_src_ids = dmcObject.video.dmcInfo.session_api.videos,
                                    }
                                }
                            }
                        }
                    },
                    keep_method = new Keep_Method()
                    {
                        heartbeat = new Heartbeat()
                        {
                            lifetime = dmcObject.video.dmcInfo.session_api.heartbeat_lifetime ?? 0,
                        }
                    },
                    priority = dmcObject.video.dmcInfo.session_api.priority,
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
                                        segment_duration = 5000,
                                        use_ssl = "no",
                                        use_well_known_port = "no",
                                    }
                                }
                            }
                        }
                    },
                    recipe_id = dmcObject.video.dmcInfo.session_api.recipe_id,
                    session_operation_auth = new Session_Operation_Auth()
                    {
                        session_operation_auth_by_signature = new Session_Operation_Auth_By_Signature()
                        {
                            signature = dmcObject.video.dmcInfo.session_api.signature,
                            token = dmcObject.video.dmcInfo.session_api.token,
                        }
                    },
                    timing_constraint = "unlimited",                    
                }
            };*/

            //var serialize = new DataContractJsonSerializer(typeof(Rootobject));
            //var memStream = new MemoryStream();
            //serialize.WriteObject(memStream, data);

            //return memStream.ToArray();

            var str = "{\"session\":{\"recipe_id\":\"nicovideo-sm33545630\",\"content_id\":\"out1\",\"content_type\":\"movie\",\"content_src_id_sets\":[{\"content_src_ids\":[{\"src_id_to_mux\":{\"video_src_ids\":[\"archive_h264_2000kbps_720p\",\"archive_h264_1600kbps_540p\",\"archive_h264_600kbps_360p\",\"archive_h264_300kbps_360p\"],\"audio_src_ids\":[\"archive_aac_192kbps\"]}}]}],\"timing_constraint\":\"unlimited\",\"keep_method\":{\"heartbeat\":{\"lifetime\":120000}},\"protocol\":{\"name\":\"http\",\"parameters\":{\"http_parameters\":{\"parameters\":{\"http_output_download_parameters\":{\"use_well_known_port\":\"no\",\"use_ssl\":\"no\",\"transfer_preset\":\"\"}}}}},\"content_uri\":\"\",\"session_operation_auth\":{\"session_operation_auth_by_signature\":{\"token\":\"{\\\"service_id\\\":\\\"nicovideo\\\",\\\"player_id\\\":\\\"nicovideo-6-RwiQgH1FaN_1531939862518\\\",\\\"recipe_id\":\"nicovideo-sm33545630\",\"service_user_id\":\"6-RwiQgH1FaN_1531939862518\",\"protocols\":[{\"name\":\"http\",\"auth_type\":\"ht2\"}],\"videos\":[\"archive_h264_1600kbps_540p\",\"archive_h264_2000kbps_720p\",\"archive_h264_300kbps_360p\",\"archive_h264_6000kbps_1080p\",\"archive_h264_600kbps_360p\"],\"audios\":[\"archive_aac_192kbps\",\"archive_aac_64kbps\"],\"movies\":[],\"created_time\":1531939862000,\"expire_time\":1532026262000,\"content_ids\":[\"out1\"],\"heartbeat_lifetime\":120000,\"content_key_timeout\":600000,\"priority\":0,\"transfer_presets\":[]}\",\"signature\":\"21a8e848d3b2b7d3395c88be8867e2e4fa2eb3c0e2dbc2c225181d9ae7042a04\"}},\"content_auth\":{\"auth_type\":\"ht2\",\"content_key_timeout\":600000,\"service_id\":\"nicovideo\",\"service_user_id\":\"6-RwiQgH1FaN_1531939862518\"},\"client_info\":{\"player_id\":\"nicovideo-6-RwiQgH1FaN_1531939862518\"},\"priority\":0}}";

            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            var urls = dmcObject?.video?.dmcInfo?.session_api?.urls;
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
