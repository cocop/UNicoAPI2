using System.Runtime.Serialization;

namespace UNicoAPI2.APIs.tag_edit.Serial
{
    /******************************************/
    /// <summary>
    /// タグ
    /// </summary>
    /******************************************/
    public class _tag//Nameメンバーが効かないので応急処置、そのうち直す
    {
        [DataMember]
        public string id;

        /// <summary>
        /// タグ名
        /// </summary>
        [DataMember]
        public string tag;

        /// <summary>
        /// タグロック
        /// </summary>
        [DataMember]
        public int owner_lock;

        /// <summary>
        /// カテゴリであるか
        /// </summary>
        [DataMember]
        public bool can_cat;

        /// <summary>
        /// can_catと同じ
        /// </summary>
        [DataMember]
        public bool? cat;

        /// <summary>
        /// 大百科が作成されているか
        /// </summary>
        [DataMember]
        public bool dic;
    }
}
