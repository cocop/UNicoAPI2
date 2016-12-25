using System.Runtime.Serialization;
using UNicoAPI2.APIs.Serial;

namespace UNicoAPI2.APIs.search.Serial
{
    /******************************************/
    /// <summary>
    /// 動画情報レスポンス
    /// </summary>
    /******************************************/
    [DataContract]
    public class contract
    {
        /// <summary>
        /// 動画情報のリスト
        /// </summary>
        [DataMember]
        public list[] list;

        /// <summary>
        /// 指定した条件で宣伝された動画を取得できるか
        /// </summary>
        [DataMember]
        public bool has_ng_video_for_adsense_on_listing;

        /// <summary>
        /// 検索したタグの説明文
        /// </summary>
        [DataMember]
        public nicopedia nicopedia;

        /// <summary>
        /// 検索したタグ一覧
        /// </summary>
        [DataMember]
        public string[] tags;

        /// <summary>
        /// 成功か失敗か
        /// </summary>
        [DataMember]
        public string status;

        /// <summary>
        /// エラーコード
        /// </summary>
        [DataMember]
        public error error;
    }
}
