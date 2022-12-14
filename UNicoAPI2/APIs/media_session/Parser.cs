using System.Text.Json;

namespace UNicoAPI2.APIs.media_session
{
    public class Parser : IParser<Request.Rootobject>
    {
        public Request.Rootobject Parse(byte[] Value)
        {
            return (Request.Rootobject)JsonSerializer.Deserialize(Value, typeof(Request.Rootobject));
        }
    }
}
