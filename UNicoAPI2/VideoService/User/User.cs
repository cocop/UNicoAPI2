namespace UNicoAPI2.VideoService.User
{
    /******************************************/
    /// <summary>ユーザー情報</summary>
    /******************************************/
    public class User
    {
        /// <summary>
        /// IDを指定して初期化
        /// </summary>
        /// <param name="ID">ユーザーID</param>
        public User(string ID)
        {
            this.ID = ID;
        }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// ユーザー名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 説明文
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// お気に入り登録された数
        /// </summary>
        public int BookmarkCount { get; set; }

        /// <summary>
        /// スタンプ経験値
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// アイコン
        /// </summary>
        public Picture Icon { get; set; }

        /// <summary>
        /// プレミアムかどうか
        /// </summary>
        public bool IsPremium { get; set; }

        /// <summary>
        /// ユーザーページ
        /// </summary>
        internal UserPage userPage;
    }
}
