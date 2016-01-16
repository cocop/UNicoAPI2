namespace UNicoAPI2.VideoService
{
    /******************************************/
    /// <summary>
    /// レスポンス
    /// </summary>
    /******************************************/
    public class Response<ResultType>
    {
        /// <summary>
        /// 通信結果
        /// </summary>
        ResultType Result;

        /// <summary>
        /// レスポンスの可否
        /// </summary>
        public Status Status { set; get; }

        /// <summary>
        /// 失敗した場合のメッセージ
        /// </summary>
        public string ErrorMessage { set; get; }
    }
}