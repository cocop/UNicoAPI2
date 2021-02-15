namespace UNicoAPI2.APIs.dmc_session.Request
{

    public class Rootobject
    {
        public Session session { get; set; }
    }

    public class Session
    {
        public string id { get; set; }
        public string recipe_id { get; set; }
        public string content_id { get; set; }
        public string content_type { get; set; }
        public Content_Src_Id_Sets[] content_src_id_sets { get; set; }
        public string timing_constraint { get; set; }
        public Keep_Method keep_method { get; set; }
        public Protocol protocol { get; set; }
        public int play_seek_time { get; set; }
        public int play_speed { get; set; }
        public Play_Control_Range play_control_range { get; set; }
        public string content_uri { get; set; }
        public Session_Operation_Auth session_operation_auth { get; set; }
        public Content_Auth content_auth { get; set; }
        public Runtime_Info runtime_info { get; set; }
        public Client_Info client_info { get; set; }
        public long created_time { get; set; }
        public long modified_time { get; set; }
        public float priority { get; set; }
        public int content_route { get; set; }
        public string version { get; set; }
        public string content_status { get; set; }
    }

    public class Keep_Method
    {
        public Heartbeat heartbeat { get; set; }
    }

    public class Heartbeat
    {
        public int lifetime { get; set; }
        public string onetime_token { get; set; }
    }

    public class Protocol
    {
        public string name { get; set; }
        public Parameters parameters { get; set; }
    }

    public class Parameters
    {
        public Http_Parameters http_parameters { get; set; }
    }

    public class Http_Parameters
    {
        public string method { get; set; }
        public Parameters1 parameters { get; set; }
    }

    public class Parameters1
    {
        public Hls_Parameters hls_parameters { get; set; }
    }

    public class Hls_Parameters
    {
        public int segment_duration { get; set; }
        public string transfer_preset { get; set; }
        public string use_ssl { get; set; }
        public string use_well_known_port { get; set; }
    }

    public class Play_Control_Range
    {
        public int max_play_speed { get; set; }
        public int min_play_speed { get; set; }
    }

    public class Session_Operation_Auth
    {
        public Session_Operation_Auth_By_Signature session_operation_auth_by_signature { get; set; }
    }

    public class Session_Operation_Auth_By_Signature
    {
        public long created_time { get; set; }
        public long expire_time { get; set; }
        public string token { get; set; }
        public string signature { get; set; }
    }

    public class Content_Auth
    {
        public string auth_type { get; set; }
        public int max_content_count { get; set; }
        public int content_key_timeout { get; set; }
        public string service_id { get; set; }
        public string service_user_id { get; set; }
        public Content_Auth_Info content_auth_info { get; set; }
    }

    public class Content_Auth_Info
    {
        public string method { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Runtime_Info
    {
        public string node_id { get; set; }
        public object[] execution_history { get; set; }
        public object[] thumbnailer_state { get; set; }
    }

    public class Client_Info
    {
        public string player_id { get; set; }
        public string remote_ip { get; set; }
        public string tracking_info { get; set; }
    }

    public class Content_Src_Id_Sets
    {
        public Content_Src_Ids[] content_src_ids { get; set; }
        public string allow_subset { get; set; }
    }

    public class Content_Src_Ids
    {
        public Src_Id_To_Mux src_id_to_mux { get; set; }
    }

    public class Src_Id_To_Mux
    {
        public string[] video_src_ids { get; set; }
        public string[] audio_src_ids { get; set; }
    }
}