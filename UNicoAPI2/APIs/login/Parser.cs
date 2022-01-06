using System;
using System.Text;
using System.Text.RegularExpressions;
using UNicoAPI2.VideoService.User;
using Windows.Foundation;

namespace UNicoAPI2.APIs.login
{
    public class Parser : IParser<User>
    {
        static readonly Regex isMultiAuthRegex = new Regex("<title>.*?段階認証.*?</title>");
        static readonly Regex getCsrfTokenRegex = new Regex("action=\"(?<value>.*?)\"");
        static readonly Regex isLoginedRegex = new Regex("<p class=\"item profile-id\"><span class=\"label\">ID</span>(?<value>[0-9].*?)</p>");

        public bool IsMultiAuth(byte[] Value)
        {
            var html = Encoding.UTF8.GetString(Value);
            var result = isMultiAuthRegex.Match(html);

            return result.Success;
        }

        public string GetCsrfToken(byte[] Value)
        {
            var html = Encoding.UTF8.GetString(Value);
            var result = getCsrfTokenRegex.Match(html);

            if (!result.Success)
                return null;


            try
            {
                var ctdecoder = new WwwFormUrlDecoder(new Uri("https://account.nicovideo.jp/" + result.Groups["value"].Value).GetComponents(UriComponents.Query, UriFormat.Unescaped));
                var ct = ctdecoder.GetFirstValueByName("continue");

                var decoder = new WwwFormUrlDecoder(new Uri(ct).GetComponents(UriComponents.Query, UriFormat.Unescaped));
                return decoder.GetFirstValueByName("csrf_token");
            }
            catch (Exception)
            {
                return null;
            }
        }

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
