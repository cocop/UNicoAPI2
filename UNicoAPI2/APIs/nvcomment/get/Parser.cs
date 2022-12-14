using System.IO;
using System.Runtime.Serialization.Json;

namespace UNicoAPI2.APIs.nvcomment.get
{
    public class Parser : IParser<Response.Rootobject>
    {
        public Response.Rootobject Parse(byte[] Value)
        {
            var serialize = new DataContractJsonSerializer(typeof(Response.Rootobject), new DataContractJsonSerializerSettings()
            {
                DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-ddTHH:mm:ssK")
            });
            return (Response.Rootobject)serialize.ReadObject(new MemoryStream(Value));
        }
    }
}
