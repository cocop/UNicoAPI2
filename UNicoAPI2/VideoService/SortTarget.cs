using System;

namespace UNicoAPI2.VideoService
{
    /******************************************/
    /// <summary>
    /// ソートに使用する値を指定する
    /// </summary>
    /******************************************/
    public enum SortTarget
    {
        /// <summary>
        /// 指定なし
        /// </summary>
        None,

        /// <summary>
        /// 最新のコメント
        /// </summary>
        Comment,

        /// <summary>
        /// 再生数
        /// </summary>
        ViewCount,

        /// <summary>
        /// コメント数
        /// </summary>
        CommentCount,

        /// <summary>
        /// マイリスト数
        /// </summary>
        MylistCount,

        /// <summary>
        /// 投稿日時
        /// </summary>
        PostTime,

        /// <summary>
        /// 動画再生時間
        /// </summary>
        VideoTime,
    }
    internal static class SortTargetForMethod
    {
        /// <summary>
        /// キーの取得
        /// </summary>
        public static string ToKey(this SortTarget SortTarget)
        {
            switch (SortTarget)
            {
                case SortTarget.None: return "n";
                case SortTarget.Comment: return "n";
                case SortTarget.ViewCount: return "v";
                case SortTarget.CommentCount: return "r";
                case SortTarget.MylistCount: return "m";
                case SortTarget.PostTime: return "f";
                case SortTarget.VideoTime: return "l";
                default: throw new Exception("設定したサーチオプションが不正です");
            }
        }
    }
}