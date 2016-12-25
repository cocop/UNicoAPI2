using System.Xml.Serialization;

namespace UNicoAPI2.APIs.upload_comment.Serial
{
    /******************************************/
    /// <summary>
    /// コメント投稿結果
    /// </summary>
    /******************************************/
    public class chat_result
    {
        /// <summary>
        /// スレッドID
        /// </summary>
        [XmlAttribute]
        public string thread;

        /// <summary>
        /// リザルトステータス
        /// </summary>
        [XmlAttribute]
        public string status;

        /// <summary>
        /// 処理終了後のラストコメントID
        /// </summary>
        [XmlAttribute]
        public int no;
    }
}