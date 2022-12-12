using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Xml.Serialization;

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
