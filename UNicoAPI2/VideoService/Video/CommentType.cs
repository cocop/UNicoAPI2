namespace UNicoAPI2.VideoService.Video
{
    /// <summary>
    /// コメントの投稿者タイプ
    /// </summary>
    public enum CommentType
    {
        /// <summary>
        /// 投稿者
        /// </summary>
        Owner,

        /// <summary>
        /// 視聴者
        /// </summary>
        User,

        /// <summary>
        /// システム(かんたんコメント)
        /// </summary>
        System,

        /// <summary>
        /// 未知のラベルが設定されていたコメント
        /// </summary>
        Unknown,
    }

    internal static class CommentTypeGen
    {
        /// <summary>
        /// ラベルから生成
        /// </summary>
        public static CommentType From(string fork)
        {
            switch (fork)
            {
                case "owner": return CommentType.Owner;
                case "main": return CommentType.User;
                case "easy": return CommentType.System;
                default: return CommentType.Unknown;
            }
        }
    }

}