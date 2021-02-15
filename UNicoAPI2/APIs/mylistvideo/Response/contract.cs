using System.Runtime.Serialization;
using UNicoAPI2.APIs.Response;

namespace UNicoAPI2.APIs.mylistvideo.Response
{
    /******************************************/
    /// <summary>
    /// マイリストレスポンス
    /// </summary>
    /******************************************/
    [DataContract]
    public class contract
    {
        /// <summary>
        /// マイリスト名
        /// </summary>
        [DataMember]
        public string name;

        /// <summary>
        /// 説明文
        /// </summary>
        [DataMember]
        public string description;

        /// <summary>
        /// ユーザーID
        /// </summary>
        [DataMember]
        public string user_id;

        /// <summary>
        /// ユーザー名
        /// </summary>
        [DataMember]
        public string user_nickname;

        /// <summary>
        /// デフォルトソート
        /// </summary>
        [DataMember]
        public string default_sort;

        /// <summary>
        /// デフォルトソート
        /// </summary>
        [DataMember]
        public list[] list;

        /// <summary>
        /// お気に入りマイリストかどうか
        /// </summary>
        [DataMember]
        public bool is_watching_this_mylist;

        /// <summary>
        /// 調査中
        /// </summary>
        [DataMember]
        public bool is_watching_count_full;

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
