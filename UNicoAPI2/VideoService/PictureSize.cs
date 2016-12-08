using System;

namespace UNicoAPI2.VideoService
{
    public enum PictureSize
    {
        /// <summary>
        /// 指定なし
        /// </summary>
        None,

        /// <summary>
        /// 中サイズ
        /// </summary>
        Medium,

        /// <summary>
        /// 大きいサイズ
        /// </summary>
        Large,
    }
    internal static class PictureSizeForMethod
    {
        /// <summary>
        /// キーの取得
        /// </summary>
        public static string ToKey(this PictureSize PictureSize)
        {
            switch (PictureSize)
            {
                case PictureSize.None: return "";
                case PictureSize.Medium: return ".M";
                case PictureSize.Large: return ".L";
                default: throw new Exception("設定したサーチオプションが不正です");
            }
        }
    }
}