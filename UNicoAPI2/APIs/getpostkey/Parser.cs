using System;
using System.Text;

namespace UNicoAPI2.APIs.getpostkey
{
    public class Parser : IParser<string>
    {
        public string Parse(byte[] Value)
        {
            var http = Encoding.UTF8.GetString(Value);

            return Uri.UnescapeDataString(http).Replace("postkey=", "");
        }
    }
}
