using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using UNicoAPI2.APIs.tag_edit.Response;

namespace UNicoAPI2.APIs.tag_edit
{
    public class Parser : IParser<contract>
    {
        public contract Parse(byte[] Value)
        {
            return (contract)JsonSerializer.Deserialize(Value, typeof(contract));
        }
    }
}
