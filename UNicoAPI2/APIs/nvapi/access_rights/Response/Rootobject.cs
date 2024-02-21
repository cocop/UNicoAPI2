using System;

namespace UNicoAPI2.APIs.nvapi.access_rights.Response
{
    public class Rootobject
    {
        public Meta meta { get; set; }
        public Data data { get; set; }
    }

    public class Meta
    {
        public int status { get; set; }
    }

    public class Data
    {
        public string contentUrl { get; set; }
        public string createTime { get; set; }
        public string expireTime { get; set; }
    }
}