using System.IO;
using System.Runtime.Serialization.Json;
using UNicoAPI2.APIs.dmc.media_session.Request;

namespace UNicoAPI2.APIs.dmc.media_session
{
    public class Parser : IParser<Rootobject>
    {
        public Rootobject Parse(byte[] Value)
        {
            var serialize = new DataContractJsonSerializer(typeof(Rootobject));
            return (Rootobject)serialize.ReadObject(new MemoryStream(Value));
        }
    }
}
