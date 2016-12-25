using System;

namespace UNicoAPI2.VideoService
{
    /******************************************/
    /// <summary>
    /// 投稿時間フィルタ
    /// </summary>
    /******************************************/
    public enum PostTimeFilter
    {
        /// <summary>
        /// 指定なし
        /// </summary>
        None,

        /// <summary>
        /// 投稿時間が24時間以内の動画のみ含める
        /// </summary>
        WithinDay,

        /// <summary>
        /// 投稿時間が一週間以内の動画のみ含める
        /// </summary>
        WithinWeek,

        /// <summary>
        /// 投稿時間が一ヶ月以内の動画のみ含める
        /// </summary>
        WithinMonth,
    }
    internal static class PostTimeFilterForMethod
    {
        /// <summary>
        /// キーの取得
        /// </summary>
        public static string ToKey(this PostTimeFilter PostTimeFilter)
        {
            switch (PostTimeFilter)
            {
                case PostTimeFilter.None: return "";
                case PostTimeFilter.WithinDay: return "1";
                case PostTimeFilter.WithinWeek: return "2";
                case PostTimeFilter.WithinMonth: return "3";
                default: throw new Exception("設定したサーチオプションが不正です");
            }
        }
    }
}