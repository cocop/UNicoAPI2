using System.Runtime.Serialization;

namespace UNicoAPI2.APIs.nvcomment.get.Request
{
    [DataContract]
    public class Rootobject
    {
        [DataMember(Name = "params")]
        public Params _params { get; set; }
        [DataMember]
        public string threadKey { get; set; }
        [DataMember]
        public Additionals additionals { get; set; }
    }

    [DataContract]
    public class Params
    {
        [DataMember(Name = "targets")]
        public Target[] targets { get; set; }
        [DataMember]
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
