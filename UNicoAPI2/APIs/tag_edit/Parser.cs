using System.IO;
using System.Runtime.Serialization.Json;

namespace UNicoAPI2.APIs.tag_edit
{
    public class Parser : IParser<Response.contract>
    {
        public Response.contract Parse(byte[] Value)
        {
            var serialize = new DataContractJsonSerializer(typeof(Response.contract));
            return (Response.contract)serialize.ReadObject(new MemoryStream(Value));
        }
    }
}
