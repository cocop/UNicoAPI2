using System.Text;
using System.Text.RegularExpressions;
using UNicoAPI2.VideoService.User;

namespace UNicoAPI2.APIs.login
{
    public class Parser : IParser<User>
    {
        static readonly Regex isLoginedRegex = new Regex("<p class=\"item profile-id\"><span class=\"label\">ID</span>(?<value>[0-9].*?)</p>");

        public User Parse(byte[] Value)
        {
            var html = Encoding.UTF8.GetString(Value);
            var result = isLoginedRegex.Match(html);

            if (!result.Success)
                return null;

            return new User(result.Groups["value"].Value);
        }
    }
}
