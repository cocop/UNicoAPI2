namespace UNicoAPI2.VideoService.Series
{
    /******************************************/
    /// <summary>
    /// マイリスト
    /// </summary>
    /******************************************/
    public class Series
    {
        /// <summary>
        /// IDを指定せず作成する
        /// </summary>
        public Series()
        {
        }

        /// <summary>
        /// IDを指定して作成する
        /// </summary>
        public Series(string ID)
        {
            this.ID = ID;
        }

        /******************************************/
        /******************************************/

        /// <summary>
        /// 製作ユーザー
        /// </summary>
        public User.User User { get; set; }

        /// <summary>
        /// シリーズID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// シリーズ動画リスト
        /// </summary>
        public Video.VideoInfo[] VideoList { get; set; }

        /// <summary>
        /// シリーズページ
        /// </summary>
        internal SeriesPage seriesPage { get; set; }
    }
}
