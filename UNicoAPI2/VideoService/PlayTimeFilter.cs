using System;

namespace UNicoAPI2.VideoService
{
    /******************************************/
    /// <summary>
    /// 再生時間フィルタ
    /// </summary>
    /******************************************/
    public enum PlayTimeFilter
    {
        /// <summary>
        /// 指定なし
        /// </summary>
        None,

        /// <summary>
        /// 再生時間が5分以内の動画のみ含める
        /// </summary>
        Short,

        /// <summary>
        /// 再生時間が20分以上の動画のみ含める
        /// </summary>
        Long,
    }
    internal static class PlayTimeFilterForMethod
    {
        /// <summary>
        /// キーの取得
        /// </summary>
        public static string ToKey(this PlayTimeFilter PlayTimeFilter)
        {
            switch (PlayTimeFilter)
            {
                case PlayTimeFilter.None: return "";
                case PlayTimeFilter.Short: return "1";
                case PlayTimeFilter.Long: return "2";
                default: throw new Exception("設定したサーチオプションが不正です");
            }
        }
    }
}