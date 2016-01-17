using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace UNicoAPI2.APIs.upload_comment
{
    public class Parser : IParser<Serial.packet>
    {
        public Serial.packet Parse(byte[] Value)
        {
            var serialize = new XmlSerializer(typeof(Serial.packet));
            return (Serial.packet)serialize.Deserialize(new MemoryStream(Value));
        }
    }
}
