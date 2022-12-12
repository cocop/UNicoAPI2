using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;

namespace UNicoAPI2.APIs.nvcomment.get
{
    public class Parser : IParser<Response.Rootobject>
    {
        public Response.Rootobject Parse(byte[] Value)
        {
            return (Response.Rootobject)JsonSerializer.Deserialize(Value, typeof(Response.Rootobject));
        }
    }
}
