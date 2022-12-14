using System.Text.Json.Serialization;

namespace UNicoAPI2.APIs.nvcomment.get.Request
{
    public class Rootobject
    {
        [JsonPropertyName("params")]
        public Params _params { get; set; }
        public string threadKey { get; set; }
        public Additionals additionals { get; set; }
    }

    public class Params
    {
        [JsonPropertyName("targets")]
        public Target[] targets { get; set; }
        public string language { get; set; }
    }

    public class Target
    {
        public string id { get; set; }
        public string fork { get; set; }
    }

    public class Additionals
    {
    }
}
