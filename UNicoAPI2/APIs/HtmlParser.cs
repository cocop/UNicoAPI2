using System.Text;

namespace UNicoAPI2.APIs
{
    /// <summary>
    /// HTMLデータの解析機
    /// </summary>
    public abstract class HtmlParser<Result> : IParser<string>
    {
        public abstract Result Parse(string Value);

        public Result HtmlParse(byte[] Value)
        {
            return Parse(Parse(Value));
        }

        public string Parse(byte[] Value)
        {
            return Encoding.UTF8.GetString(Value);
        }
    }
}
