using System;

namespace UNicoAPI2.VideoService
{
    /******************************************/
    /// <summary>
    /// ソートの並び順
    /// </summary>
    /******************************************/
    public enum SortOrder
    {
        /// <summary>
        /// 指定なし
        /// </summary>
        None,

        /// <summary>
        /// 昇順
        /// </summary>
        Up,

        /// <summary>
        /// 降順
        /// </summary>
        Down,
    }
    internal static class SortOrderForMethod
    {
        /// <summary>
        /// キーの取得
        /// </summary>
        public static string ToKey(this SortOrder SortOrder)
        {
            switch (SortOrder)
            {
                case SortOrder.None: return "d";
                case SortOrder.Up: return "d";
                case SortOrder.Down: return "a";
                default: throw new Exception("設定したサーチオプションが不正です");
            }
        }
    }
}