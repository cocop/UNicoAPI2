using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.likes
{
    public class DoParser : IParser<Response.Rootobject>
    {
        public Response.Rootobject Parse(byte[] Value)
        {
            var serialize = new DataContractJsonSerializer(typeof(Response.Rootobject));
            return (Response.Rootobject)serialize.ReadObject(new MemoryStream(Value));
        }
    }
}
