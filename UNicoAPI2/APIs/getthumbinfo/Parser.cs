using System.IO;
using System.Xml.Serialization;

namespace UNicoAPI2.APIs.getthumbinfo
{
    public class Parser : IParser<Response.nicovideo_thumb_response>
    {
        public Response.nicovideo_thumb_response Parse(byte[] Value)
        {
            var serialize = new XmlSerializer(typeof(Response.nicovideo_thumb_response));
            return (Response.nicovideo_thumb_response)serialize.Deserialize(new MemoryStream(Value));
        }
    }
}
