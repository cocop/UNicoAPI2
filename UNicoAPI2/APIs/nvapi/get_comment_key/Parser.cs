using System.Text.Json;

namespace UNicoAPI2.APIs.nvapi.get_comment_key
{
    public class Parser : IParser<Response.Rootobject>
    {
        public Response.Rootobject Parse(byte[] Value)
        {
            return (Response.Rootobject)JsonSerializer.Deserialize(Value, typeof(Response.Rootobject));
        }
    }
}
