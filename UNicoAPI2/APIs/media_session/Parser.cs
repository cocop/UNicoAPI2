using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace UNicoAPI2.APIs.media_session
{
    public class Parser : IParser<Request.Rootobject>
    {
        public Request.Rootobject Parse(byte[] Value)
        {
            var serialize = new DataContractJsonSerializer(typeof(Request.Rootobject));
            return (Request.Rootobject)serialize.ReadObject(new MemoryStream(Value));
        }
    }
}
