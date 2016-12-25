using System.Runtime.Serialization;

namespace UNicoAPI2.APIs.mylistvideo.Serial
{
    /// <summary>
    /// サムネイルスタイル
    /// </summary>
    [DataContract]
    public class thumbnail_style
    {
        /// <summary>
        /// オフセットX
        /// </summary>
        [DataMember]
        public int offset_x;

        /// <summary>
        /// オフセットY
        /// </summary>
        [DataMember]
        public int offset_y;

        /// <summary>
        /// 幅
        /// </summary>
        [DataMember]
        public int width;
    }
}