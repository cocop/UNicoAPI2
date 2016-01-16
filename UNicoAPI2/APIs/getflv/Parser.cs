using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace UNicoAPI2.APIs.getflv
{
    public class Parser : IParser<NameValueCollection>
    {
        public NameValueCollection Parse(byte[] Value)
        {
            var text = Uri.UnescapeDataString(Encoding.UTF8.GetString(Value));
            var texts = text.Split('&');
            var result = new NameValueCollection();

            for (int i = 0; i < texts.Length; i++)
            {
                var index = texts[i].IndexOf('=');
                result.Add(
                    texts[i].Substring(0, index),
                    texts[i].Substring(index + 1, texts[i].Length - 1 - index));
            }

            return result;
        }
    }
}
