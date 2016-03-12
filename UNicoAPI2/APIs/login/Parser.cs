using System.Text;

namespace UNicoAPI2.APIs.login
{
    public class Parser : IParser<bool>
    {
        public bool Parse(byte[] Value)
        {
            var http = Encoding.UTF8.GetString(Value);

            return !(
                http.Contains("ログインエラー") ||
                http.Contains("間違っています"));
        }
    }
}
