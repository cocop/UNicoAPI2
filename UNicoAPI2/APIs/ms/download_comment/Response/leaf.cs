using System.Xml.Serialization;

namespace UNicoAPI2.APIs.ms.download_comment.Response
{
    /******************************************/
    /// <summary>
    /// コメントリーフ情報
    /// </summary>
    /******************************************/
    public class leaf
    {
        /// <summary>
        /// コメントリーフID
        /// </summary>
        [XmlAttribute("leaf")]
        public int _leaf;

        /// <summary>
        /// スレッドID
        /// </summary>
        [XmlAttribute]
        public int thread;

        /// <summary>
        /// リーフ内の総コメント数
        /// </summary>
        [XmlAttribute]
        public int count;
    }
}