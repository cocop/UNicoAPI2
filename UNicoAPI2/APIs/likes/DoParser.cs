using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.likes
{
    public class DoParser : IParser<Response.Rootobject>
    {
        public Response.Rootobject Parse(byte[] Value)
        {
            return (Response.Rootobject)JsonSerializer.Deserialize(Value, typeof(Response.Rootobject));
        }
    }
}
