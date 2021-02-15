using System.Xml.Serialization;

namespace UNicoAPI2.APIs.download_comment.Response
{
    /******************************************/
    /// <summary>
    /// コメントレスポンス
    /// </summary>
    /******************************************/
    [XmlRoot]
    public class packet
    {
        /// <summary>
        /// コメントリーフ情報
        /// </summary>
        [XmlElement]
        public leaf[] leaf;

        /// <summary>
        /// スレッド情報
        /// </summary>
        [XmlElement]
        public thread[] thread;

        /// <summary>
        /// コメント周りのカウンタ
        /// </summary>
        [XmlElement]
        public view_counter view_counter;

        /// <summary>
        /// コメント
        /// </summary>
        [XmlElement]
        public chat[] chat;
    }
}
