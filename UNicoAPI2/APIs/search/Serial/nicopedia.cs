using System.Runtime.Serialization;

namespace UNicoAPI2.APIs.search.Serial
{
    /// <summary>
    /// ニコニコ大百科
    /// </summary>
    public class nicopedia
    {
        /// <summary>
        /// タグ説明、一番最初に指定したタグのニコニコ大百科から参照される
        /// </summary>
        [DataMember]
        public string html;
    }
}