using System.Runtime.Serialization;

namespace UNicoAPI2.APIs.tag_edit.Response
{
    /******************************************/
    /// <summary>
    /// タグ
    /// </summary>
    /******************************************/
    [DataContract]
    public class contract
    {
        [DataMember]
        public bool is_owner;

        /// <summary>
        /// 編集可能か
        /// </summary>
        [DataMember]
        public bool isuneditable_tag;

        /// <summary>
        /// タグ情報
        /// </summary>
        [DataMember]
        public _tag[] tags;

        /// <summary>
        /// 成功か失敗か
        /// </summary>
        [DataMember]
        public string status;

        /// <summary>
        /// エラーコード
        /// </summary>
        [DataMember]
        public string error_msg;
    }
}
