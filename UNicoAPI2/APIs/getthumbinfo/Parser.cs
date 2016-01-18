using System.IO;
using System.Xml.Serialization;

namespace UNicoAPI2.APIs.getthumbinfo
{
    public class Parser : IParser<Serial.nicovideo_thumb_response>
    {
        public Serial.nicovideo_thumb_response Parse(byte[] Value)
        {
            var serialize = new XmlSerializer(typeof(Serial.nicovideo_thumb_response));
            return (Serial.nicovideo_thumb_response)serialize.Deserialize(new MemoryStream(Value));
        }
    }
}
