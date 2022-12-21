using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace UNicoAPI2.APIs.login
{
    public class Parser : IParser<Dictionary<string, string>>
    {
        static readonly Regex isMultiAuthRegex = new Regex("<title>.*?段階認証.*?</title>");
        static readonly Regex getCsrfTokenRegex = new Regex("action=\"(?<value>.*?)\"");

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
                var ctMatch = new Regex("csrf_token=(?<value>.*?)(&|$)").Match(
                    HttpUtility.UrlDecode(result.Groups["value"].Value));

                return ctMatch.Groups["value"].Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Dictionary<string, string> Parse(byte[] Value)
        {
            var parser = new html.my_page.Parser();
            return parser.Parse(parser.Parse(Value));
        }
    }
}
