using System.Text.Json;

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
