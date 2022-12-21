using System.IO;
using System.Runtime.Serialization.Json;
using UNicoAPI2.APIs.dmc.heartbeats.Response;


namespace UNicoAPI2.APIs.dmc.heartbeats
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
