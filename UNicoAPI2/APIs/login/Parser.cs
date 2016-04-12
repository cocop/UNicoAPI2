using System.Text;
using System.Text.RegularExpressions;

namespace UNicoAPI2.APIs.login
{
    public class Parser : IParser<string>
    {
        static readonly Regex regex = new Regex("<p class=\"item profile-id\"><span class=\"label\">ID</span>(?<value>[0-9].*?)</p>");

        public string Parse(byte[] Value)
        {
            var http = Encoding.UTF8.GetString(Value);

            if (http.Contains("ログインエラー") || http.Contains("間違っています"))
                return null;

            return regex.Match(http).Groups["value"].Value;
        }
    }
}
