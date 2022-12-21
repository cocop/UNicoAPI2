using System.IO;
using System.Xml.Serialization;

namespace UNicoAPI2.APIs.ms.download_comment
{
    public class Parser : IParser<Response.packet>
    {
        public Response.packet Parse(byte[] Value)
        {
            var serialize = new XmlSerializer(typeof(Response.packet));
            return (Response.packet)serialize.Deserialize(new MemoryStream(Value));
        }
    }
}
