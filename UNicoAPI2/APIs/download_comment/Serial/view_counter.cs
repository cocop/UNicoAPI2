using System.Xml.Serialization;

namespace UNicoAPI2.APIs.download_comment.Serial
{
    /******************************************/
    /// <summary>
    /// 動画周りのカウンタ
    /// </summary>
    /******************************************/
    public class view_counter
    {
        /// <summary>
        /// 再生数
        /// </summary>
        [XmlAttribute]
        public int video;

        /// <summary>
        /// 動画ID
        /// </summary>
        [XmlAttribute]
        public string id;

        /// <summary>
        /// マイリスト登録数
        /// </summary>
        [XmlAttribute]
        public int mylist;
    }
}