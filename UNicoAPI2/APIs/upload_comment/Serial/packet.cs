using System.Xml.Serialization;

namespace UNicoAPI2.APIs.upload_comment.Serial
{
    /******************************************/
    /// <summary>
    /// コメント投稿結果
    /// </summary>
    /******************************************/
    [XmlRoot]
    public class packet
    {
        /// <summary>
        /// コメント投稿結果
        /// </summary>
        [XmlElement]
        public chat_result chat_result;
    }
}
