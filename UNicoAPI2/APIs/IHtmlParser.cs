namespace UNicoAPI2.APIs
{
    /// <summary>
    /// HTMLデータの解析機
    /// </summary>
    interface IHtmlParser<Result> : IParser<string>
    {
        Result Parse(string Value);
    }
}
