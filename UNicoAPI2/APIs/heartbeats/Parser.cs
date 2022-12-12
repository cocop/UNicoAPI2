using System.IO;
using System.Text.Json;


namespace UNicoAPI2.APIs.heartbeats
{
    public class Parser : IParser<Response.Rootobject>
    {
        public Response.Rootobject Parse(byte[] Value)
        {
            return (Response.Rootobject)JsonSerializer.Deserialize(Value, typeof(Response.Rootobject));
        }
    }
}
