namespace UNicoAPI2.APIs
{
    /// <summary>
    /// 出力データの解析機
    /// </summary>
    public interface IParser<Result>
    {
        Result Parse(byte[] Value);
    }
}
