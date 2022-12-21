using System.IO;
using System.Xml.Serialization;
using UNicoAPI2.APIs.ms.getthumbinfo.Response;

namespace UNicoAPI2.APIs.ms.getthumbinfo
{
    public class Parser : IParser<nicovideo_thumb_response>
    {
        public nicovideo_thumb_response Parse(byte[] Value)
        {
            var serialize = new XmlSerializer(typeof(nicovideo_thumb_response));
            return (nicovideo_thumb_response)serialize.Deserialize(new MemoryStream(Value));
        }
    }
}
