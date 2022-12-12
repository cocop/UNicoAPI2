using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UNicoAPI2.APIs.nvcomment.post.Request
{
    public class Rootobject
    {
        public string videoId { get; set; }
        public string[] commands { get; set; }
        public string body { get; set; }
        public int vposMs { get; set; }
        public string postKey { get; set; }
    }
}
