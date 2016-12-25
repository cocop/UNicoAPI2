namespace UNicoAPI2.VideoService
{
    /******************************************/
    /// <summary>
    /// 検索オプション
    /// </summary>
    /******************************************/
    public partial class SearchOption
    {
        /// <summary>
        /// ソート順
        /// </summary>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// ソートに使用する値
        /// </summary>
        public SortTarget SortTarget { get; set; }

        /// <summary>
        /// 投稿時間
        /// </summary>
        public PostTimeFilter PostTimeFilter { get; set; }

        /// <summary>
        /// 動画時間
        /// </summary>
        public PlayTimeFilter PlayTimeFilter { get; set; }
    }
}