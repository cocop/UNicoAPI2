using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using UNicoAPI2.APIs.tag_edit.Serial;

namespace UNicoAPI2.APIs.tag_edit
{
    public class Parser : IParser<contract>
    {
        public contract Parse(byte[] Value)
        {
            var json = Encoding.UTF8.GetString(Value);
            var serialize = new DataContractJsonSerializer(typeof(contract));

            return (contract)serialize.ReadObject(new MemoryStream(Value));
        }
    }
}
