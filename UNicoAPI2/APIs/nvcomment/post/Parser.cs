using System.IO;
using System.Runtime.Serialization.Json;

namespace UNicoAPI2.APIs.nvcomment.post
{
    public class Parser : IParser<Response.Rootobject>
    {
        public Response.Rootobject Parse(byte[] Value)
        {
            var serialize = new DataContractJsonSerializer(typeof(Response.Rootobject));
            return (Response.Rootobject)serialize.ReadObject(new MemoryStream(Value));
        }
    }
}
