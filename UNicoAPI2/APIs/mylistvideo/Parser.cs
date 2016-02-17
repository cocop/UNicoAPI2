using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace UNicoAPI2.APIs.mylistvideo
{
    public class Parser : IParser<Serial.contract>
    {
        public Serial.contract Parse(byte[] Value)
        {
            var serialize = new DataContractJsonSerializer(typeof(Serial.contract));
            return (Serial.contract)serialize.ReadObject(new MemoryStream(Value));
        }
    }
}
