using System.IO;
using System.Xml.Serialization;
using UNicoAPI2.APIs.ms.upload_comment.Response;

namespace UNicoAPI2.APIs.ms.upload_comment
{
    public class Parser : IParser<packet>
    {
        public packet Parse(byte[] Value)
        {
            var serialize = new XmlSerializer(typeof(packet));
            return (packet)serialize.Deserialize(new MemoryStream(Value));
        }
    }
}
