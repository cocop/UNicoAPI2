using System.Text;

namespace UNicoAPI2.APIs.html.login_page
{
    public class Parser : IParser<string>
    {
        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }
    }
}
