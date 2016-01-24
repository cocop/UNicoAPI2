namespace UNicoAPI2.VideoService
{
    /******************************************/
    /// <summary>
    /// レスポンス
    /// </summary>
    /******************************************/
    public class Response
    {
        /// <summary>
        /// レスポンスの可否
        /// </summary>
        public Status Status { set; get; }

        /// <summary>
        /// 失敗した場合のメッセージ
        /// </summary>
        public string ErrorMessage { set; get; }
    }

    /******************************************/
    /// <summary>
    /// レスポンス
    /// </summary>
    /******************************************/
    public class Response<ResultType> : Response
    {
        /// <summary>
        /// 通信結果
        /// </summary>
        public ResultType Result { set; get; }
    }
}